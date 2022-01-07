using Proiect_UrsuAlexandra.Models;
using System;
using System.Linq;

namespace Proiect_UrsuAlexandra.Data
{
    public class DbInitializer
    {
        public static void Initialize(AgencyContext context)
        {
            context.Database.EnsureCreated();

            if (context.Jobs.Any())
            {
                return; // BD a fost creata anterior
            }

            var companies = new Company[]
           {

                new Company{CompanyName="NTT Data",Adress="Str. Aviatorilor, nr. 40, Bucuresti"},
                new Company{CompanyName="Endava",Adress="Str. Plopilor, nr. 35, Ploiesti"},
                new Company{CompanyName="Fortech",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"}
           };
            foreach (Company c in companies)
            {
                context.Companies.Add(c);
            }
            context.SaveChanges();

            var jobs = new Job[]
            {
                new Job{Title="Java Developer Senior",Experience_Required="L6", WorkDay_Hours=8, Location="Cluj", Salary=Decimal.Parse("1800")},
                new Job{Title="Financial Consultant",Experience_Required="L1", WorkDay_Hours=6, Location="Sibiu", Salary = Decimal.Parse("650") },
                new Job{Title="Business Analist",Experience_Required="L2", WorkDay_Hours=8, Location="Cluj", Salary=Decimal.Parse("1250")},
                new Job{Title="Java Developer",Experience_Required="L1", WorkDay_Hours=8, Location="Bucuresti", Salary=Decimal.Parse("1000")},
                new Job{Title="Project Manager",Experience_Required="L6", WorkDay_Hours=8, Location="Cluj", Salary = Decimal.Parse("6000") },
                new Job{Title="Business Analist Senior",Experience_Required="L4", WorkDay_Hours=8, Location="Brasov", Salary=Decimal.Parse("2500")}
            };

            foreach (Job j in jobs)
            {
                context.Jobs.Add(j);
            }
            context.SaveChanges();

            var persons = new Person[]
            {
                new Person{PersonID=1050,FirstName="Popescu",LastName="Marcela", Domain="Java", Experience="L3", BirthDate=DateTime.Parse("1979-09-01"), City="CLuj"},
                new Person{PersonID=1045,FirstName="Mihailescu", LastName="Cornel", Domain="Data Analyst", Experience="L2", BirthDate=DateTime.Parse("1969-07-08"), City="Cluj"}

            };

            foreach (Person p in persons)
            {
                context.Persons.Add(p);
            }
            context.SaveChanges();

            var applications = new Application[]
            {
                new Application{JobID=1,PersonID=1050, ApplicationDate=DateTime.Parse("2020-09-01")},
                new Application{JobID=3,PersonID=1045, ApplicationDate=DateTime.Parse("2020-09-01")},
                new Application{JobID=1,PersonID=1045, ApplicationDate=DateTime.Parse("2020-09-01")},
                new Application{JobID=2,PersonID=1050, ApplicationDate=DateTime.Parse("2020-09-01")},
                new Application{JobID=4,PersonID=1050, ApplicationDate=DateTime.Parse("2020-09-01")},
                new Application{JobID=6,PersonID=1050, ApplicationDate=DateTime.Parse("2020-09-01")}
            };

            foreach (Application e in applications)
            {
                context.Applications.Add(e);
                Console.WriteLine(e.PersonID);
            }
            context.SaveChanges();

           

            var listedJob = new ListedJob[]
            {
                new ListedJob {JobID = jobs.Single(j => j.Title == "Java Developer" ).ID, CompanyID = companies.Single(i => i.CompanyName =="NTT Data").ID},
                new ListedJob {JobID = jobs.Single(j => j.Title == "Business Analist" ).ID, CompanyID = companies.Single(i => i.CompanyName =="NTT Data").ID},
                new ListedJob {JobID = jobs.Single(j => j.Title == "Project Manager" ).ID, CompanyID = companies.Single(i => i.CompanyName =="Endava").ID},
                new ListedJob {JobID = jobs.Single(j => j.Title == "Financial Consultant" ).ID, CompanyID = companies.Single(i => i.CompanyName =="Endava").ID},
                new ListedJob {JobID = jobs.Single(j => j.Title == "Java Developer" ).ID, CompanyID = companies.Single(i => i.CompanyName =="Fortech").ID},
                new ListedJob {JobID = jobs.Single(j => j.Title == "Business Analist" ).ID, CompanyID = companies.Single(i => i.CompanyName =="Fortech").ID}

            };
            foreach (ListedJob lj in listedJob)
            {
                context.ListedJobs.Add(lj);
            }
            context.SaveChanges();


        }
    }
}
