using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardCMS.DB.Models
{
    public class Lead
    {
        [Key]
        public int LeadId { get; set; } // Primary key

        [Required]
        public string CustomerName { get; set; }

        [Required]
        [Phone]
        public string CustomerPhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; } // Maps to nVARCHAR(5000)

        [Required]
        public string PlotNo { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal LeadPrice { get; set; }

        public DateTime LeadDate { get; set; }

        // Foreign Key for LeadStatus
        public int StatusId { get; set; }

        // Navigation property to LeadStatus
        public LeadStatus LeadStatus { get; set; } // Navigation property to access the related LeadStatus

        public string LeadUpdates { get; set; } // Maps to nVARCHAR(MAX)
    }
}
