using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;   

namespace Proiect_UrsuAlexandra.Models
{

    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Domain { get; set; }
        public string Experience { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
