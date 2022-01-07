using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_UrsuAlexandra.Models
{
    public class Job
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        public string Experience_Required { get; set; }
        public int WorkDay_Hours { get; set; }
        public string Location { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Salary { get; set; }

        public ICollection<Application> Applications { get; set; }
        public ICollection<ListedJob> ListedJob { get; set; }

    }
}
