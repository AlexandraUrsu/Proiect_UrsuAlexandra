using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_UrsuAlexandra.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public int JobID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public Person Person { get; set; }
        public Job Job { get; set; }

    }
}
