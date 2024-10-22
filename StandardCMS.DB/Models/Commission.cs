using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardCMS.DB.Models
{
    public class Commission
    {
        public int CommissionId { get; set; }
        public int SaleId { get; set; }
        public int AgentId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CommissionAmount { get; set; }
        public string CommissionType { get; set; }
        public DateTime DateIssued { get; set; }

        public Sale Sale { get; set; }
        public Agent Agent { get; set; }
    }
}
