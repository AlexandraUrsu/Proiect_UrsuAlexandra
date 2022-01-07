using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_UrsuAlexandra.Data;
using Proiect_UrsuAlexandra.Models;

namespace Proiect_UrsuAlexandra.Controllers
{
    public class JobsController : Controller
    {
        private readonly AgencyContext _context;

        public JobsController(AgencyContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["Experience_RequiredSortParm"] = String.IsNullOrEmpty(sortOrder) ? "exp_desc" : "";
            ViewData["WorkDay_HoursSortParm"] = sortOrder == "WorkDay_Hours" ? "work_desc" : "WorkDay_Hours";
            ViewData["LocationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
            ViewData["SalarySortParm"] = sortOrder == "Salary" ? "salary_desc" : "Salary";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var jobs = from j in _context.Jobs
                        select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    jobs = jobs.OrderByDescending(j => j.Title);
                    break;
                case "exp_desc":
                    jobs = jobs.OrderByDescending(j => j.Experience_Required);
                    break;
                case "WorkDay_Hours":
                    jobs = jobs.OrderBy(j => j.WorkDay_Hours);
                    break;
                case "work_desc":
                    jobs = jobs.OrderByDescending(j => j.WorkDay_Hours);
                    break;
                case "location_desc":
                    jobs = jobs.OrderByDescending(j => j.Location);
                    break;
                case "Salary":
                    jobs = jobs.OrderBy(j => j.Salary);
                    break;
                case "salary_desc":
                    jobs = jobs.OrderByDescending(j => j.Salary);
                    break;
                default:
                    jobs = jobs.OrderBy(j => j.Title);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Job>.CreateAsync(jobs.AsNoTracking(), pageNumber ??
           1, pageSize));
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
            .Include(s => s.Applications)
            .ThenInclude(e => e.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);


            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Experience_Required,WorkDay_Hours,Location,Salary")] Job job)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobToUpdate = await _context.Jobs.FirstOrDefaultAsync(j => j.ID == id);
            if (await TryUpdateModelAsync<Job>(
            jobToUpdate,
            "",
            j => j.Title, j => j.Experience_Required, j => j.WorkDay_Hours, j => j.Location, j => j.Salary))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(jobToUpdate);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.ID == id);
        }
    }
}
