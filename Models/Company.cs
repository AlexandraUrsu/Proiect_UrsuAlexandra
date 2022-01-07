using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proiect_UrsuAlexandra.Models
{

    public class Company
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<ListedJob> ListedJob { get; set; }

    }
}