using System.Collections.Generic;

namespace Proiect_UrsuAlexandra.Models.AgencyViewModels
{
    public class PersonIndexData
    {
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Application> Applications { get; set; }
    }
}
