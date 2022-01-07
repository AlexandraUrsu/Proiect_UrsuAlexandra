
using System;
using System.ComponentModel.DataAnnotations;
namespace Proiect_UrsuAlexandra.Models.LibraryViewModels
{
    public class ApplicationGroup
    {
        //[DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; }
        public int JobCount { get; set; }

    }
}