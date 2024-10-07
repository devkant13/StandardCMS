using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardCMS.DB.Models
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string Name { get; set; }
        public int? ParentAgentId { get; set; }  // Nullable for the root agent
        public virtual Agent ParentAgent { get; set; }  // Self-referencing relationship // Reference to parent agent
        public virtual ICollection<Agent> ChildAgents { get; set; }   // For child agents
        // Navigation property for related Commissions
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }
}
