using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardCMS.DB.Models
{
    public class CommissionRule
    {
        public int CommissionRuleId { get; set; }
        public int AgentLevel { get; set; }  // L1, L2, etc.
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CommissionPercentage { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SaleAmount { get; set; } //added newly as comission Varies on SaleAmount
    }
}
