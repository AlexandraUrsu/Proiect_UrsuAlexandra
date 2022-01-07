using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_UrsuAlexandra.Data;
using Proiect_UrsuAlexandra.Models;
using Proiect_UrsuAlexandra.Models.AgencyViewModels;

namespace Proiect_UrsuAlexandra.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AgencyContext _context;

        public CompaniesController(AgencyContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index(int? id, int? jobID)
        {
            var viewModel = new CompanyIndexData();
            viewModel.Companies = await _context.Companies
            .Include(i => i.ListedJob)
            .ThenInclude(i => i.Job)
            .ThenInclude(i => i.Applications)
            .ThenInclude(i => i.Person)
            .AsNoTracking()
            .OrderBy(i => i.CompanyName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["CopmanyID"] = id.Value;
                Company company = viewModel.Companies.Where(
                i => i.ID == id.Value).Single();
                viewModel.Jobs = company.ListedJob.Select(s => s.Job);
            }
            if (jobID != null)
            {
                ViewData["JobID"] = jobID.Value;
                viewModel.Applications = viewModel.Jobs.Where(
                x => x.ID == jobID).Single().Applications;
            }
            return View(viewModel);
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CompanyName,Adress")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                var company = await _context.Companies
                .Include(i => i.ListedJob).ThenInclude(i => i.Job)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
                if (company == null)
                {
                    return NotFound();
                }
                PopulateListedJobData(company);
                return View(company);
            }
        }

        private void PopulateListedJobData(Company company)
        {
            var allJobs = _context.Jobs;
            var listedJobs = new HashSet<int>(company.ListedJob.Select(c => c.JobID));
            var viewModel = new List<ListedJobData>();
            foreach (var job in allJobs)
            {
                viewModel.Add(new ListedJobData
                {
                    JobID = job.ID,
                    Title = job.Title,
                    IsListed = listedJobs.Contains(job.ID)
                });
            }
            ViewData["Jobs"] = viewModel;
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedJobs)
        {
            if(id == null)
            {
                return NotFound();
            }
            var companyToUpdate = await _context.Companies
            .Include(i => i.ListedJob)
            .ThenInclude(i => i.Job)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Company>(companyToUpdate, "",i => i.CompanyName, i => i.Adress))
            {
                UpdateListedJobs(selectedJobs, companyToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateListedJobs(selectedJobs, companyToUpdate);
            PopulateListedJobData(companyToUpdate);
            return View(companyToUpdate);
        }

        private void UpdateListedJobs(string[] selectedJobs, Company companyToUpdate)
        {
            if (selectedJobs == null)
            {
                companyToUpdate.ListedJob = new List<ListedJob>();
                return;
            }
            var selectedJobsHS = new HashSet<string>(selectedJobs);
            var listedJobs = new HashSet<int>
            (companyToUpdate.ListedJob.Select(c => c.Job.ID));
            foreach (var job in _context.Jobs)
            {
                if (selectedJobsHS.Contains(job.ID.ToString()))
                {
                    if (!listedJobs.Contains(job.ID))
                    {
                        companyToUpdate.ListedJob.Add(new ListedJob
                        {
                            CompanyID = companyToUpdate.ID,
                            JobID = job.ID
                        });
                    }
                }
                else
                {
                    if (listedJobs.Contains(job.ID))
                    {
                        ListedJob jobToRemove = companyToUpdate.ListedJob.FirstOrDefault(i
                       => i.JobID == job.ID);
                        _context.Remove(jobToRemove);
                    }
                }
            }
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.ID == id);
        }
    }
}
