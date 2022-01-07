namespace Proiect_UrsuAlexandra.Models
{
    public class ListedJob
    {
        public int CompanyID { get; set; }
        public int JobID { get; set; }
        public Company Company { get; set; }
        public Job Job { get; set; }
    }
}
