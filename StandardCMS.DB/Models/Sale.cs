using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardCMS.DB.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int AgentId { get; set; }

        [Column(TypeName = "decimal(18, 4)")] 
        public decimal SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual ICollection<Commission> Commissions { get; set; }  // Commissions related to this sale

    }
}
