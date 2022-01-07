using System.Collections.Generic;

namespace Proiect_UrsuAlexandra.Models.AgencyViewModels
{
    public class CompanyIndexData
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Application> Applications { get; set; }
    }
}
