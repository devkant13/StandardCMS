using Microsoft.EntityFrameworkCore;
using StandardCMS.DB;
using StandardCMS.DB.Models;
using System;
using static WebApp.Utility.Utility;

namespace WebApp.Areas.Admin
{
    public class CommissionService : ICommissionService
    {
        private readonly ApplicationDbContext _context;

        public CommissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        private decimal CalculateCommission(decimal saleAmount)
        {
            //var rule = _context.CommissionRules
            //                   .FirstOrDefault(r => saleAmount > r.MinAmount && (r.MaxAmount == null || saleAmount <= r.MaxAmount));
            var rule = _context.CommissionRules
                               .FirstOrDefault(r => saleAmount > 0 && (r.SaleAmount == null || saleAmount <= r.SaleAmount));

            return rule != null ? (rule.CommissionPercentage / 100) * saleAmount : 0;
        }

        public async Task ApplyCommission(Sale sale)
        {
            try
            {


                int agentId = sale.AgentId; decimal saleAmount = sale.SaleAmount;

                var agent = _context.Agents.Include(a => a.ParentAgent).ThenInclude(a => a.ParentAgent).FirstOrDefault(a => a.AgentId == agentId);
                if (agent == null) throw new Exception("Agent not found");

                // Calculate commission for selling agent
                decimal agentCommission = CalculateCommission(saleAmount);

                // Apply 1% royalty to parent agents (up to two levels)
                decimal parentCommission = (1m / 100) * saleAmount;

                // Add agent commission logic here (save to database, etc.)
                //Save Commission for Agent
                _context.Commissions.Add(new Commission
                {
                    SaleId = sale.SaleId,
                    AgentId = agent.AgentId,
                    CommissionAmount = agentCommission,
                    CommissionType = CommissionType.OwnSale.ToString(),
                    DateIssued = sale.SaleDate
                });
                await _context.SaveChangesAsync();

                if (agent.ParentAgent != null)
                {
                    // Apply 1% royalty to the direct parent agent
                    decimal parent1Commission = parentCommission;
                    var parent1Id = agent.ParentAgent.AgentId;
                    var Chkparent1Id = agent.ParentAgentId;
                    //Save Commission for parent1
                    _context.Commissions.Add(new Commission
                    {
                        SaleId = sale.SaleId,
                        AgentId = agent.ParentAgent.AgentId,
                        CommissionAmount = parent1Commission,
                        CommissionType = CommissionType.Parent.ToString(),
                        DateIssued = sale.SaleDate
                    });
                    await _context.SaveChangesAsync();

                    // If parent has another parent (grandparent), apply royalty
                    if (agent.ParentAgent.ParentAgent != null)
                    {
                        decimal parent2Commission = parentCommission;
                        // Save both parent1 and parent2 commissions to the database
                        //TODO: 1.saving commission to under DB trnsaction.  2.Add coulmn "CommissionType"     
                        
                        //Save Commission for parent2
                        _context.Commissions.Add(new Commission
                        {
                            SaleId = sale.SaleId,
                            AgentId = agent.ParentAgent.ParentAgent.AgentId,
                            CommissionAmount = parent2Commission,
                            CommissionType = CommissionType.Parent.ToString(),
                            DateIssued = sale.SaleDate
                        });
                        await _context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex )
            {
                var ww = ex.ToString();
                throw;
            }
        }
    }

}
