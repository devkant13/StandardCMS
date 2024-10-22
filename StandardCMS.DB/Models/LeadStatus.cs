using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardCMS.DB.Models
{
    public class LeadStatus
    {
        [Key]
        public int ID { get; set; } // Primary key

        [Required]
        public string Status { get; set; } // Status of the lead (e.g., Open, Closed)

        public string Description { get; set; } // Description of the status

        // Navigation property for related leads
        public ICollection<Lead> Leads { get; set; }
    }
}
