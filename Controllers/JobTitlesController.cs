using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.Models;

namespace MKsEMS.Controllers
{
    public class JobTitlesController : Controller
    {
        private readonly EMSDbContext _context;

        public JobTitlesController(EMSDbContext context)
        {
            _context = context;
        }

        // GET: JobTitles
        /// <summary>
        /// Displaying a list of all Active Job Titles
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            return _context.JobTitles != null ? 
                          View(await _context.JobTitles.Where(j => j.IsActive).ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.JobTitles'  is null.");
        }

        // GET: JobTitles/Details/5
        /// <summary>
        /// Details of a selected Job Title
        /// </summary>
        /// <param name="id">Id of the selected Job Title</param>
        /// <returns>Details of the selected Job Title</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;
           
            if (id == null || _context.JobTitles == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // GET: JobTitles/Create
        /// <summary>
        /// Filling out create Job Title form
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            return View();
        }

        // POST: JobTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creating the Job Title
        /// </summary>
        /// <param name="jobTitle">Job Title details filled out by Administrator</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] JobTitle jobTitle)
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            if (ModelState.IsValid)
            {
                _context.Add(jobTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobTitle);
        }

        // GET: JobTitles/Edit/5
        /// <summary>
        /// Filling out the Job Title details on the form
        /// </summary>
        /// <param name="id">Id of the selected Job Title</param>
        /// <returns>JobTitle object</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            if (id == null || _context.JobTitles == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitles.FindAsync(id);
            if (jobTitle == null)
            {
                return NotFound();
            }
            return View(jobTitle);
        }

        // POST: JobTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Editing the filled out Job Title
        /// </summary>
        /// <param name="id">Id of the Job Title to be edited</param>
        /// <param name="jobTitle">JobTitle object of the Job Title to be edited</param>
        /// <returns>JobTitle object</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] JobTitle jobTitle)
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            if (id != jobTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTitleExists(jobTitle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobTitle);
        }

        // GET: JobTitles/Delete/5
        /// <summary>
        /// Deleting a JobTitle object to be deleted
        /// </summary>
        /// <param name="id">Id of the selected JobTitle</param>
        /// <returns>JobTitle object to be deleted</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            if (id == null || _context.JobTitles == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // POST: JobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AdminUserIsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in as Administratro;

            if (_context.JobTitles == null)
            {
                return Problem("Entity set 'EMSDbContext.JobTitles'  is null.");
            }
            var jobTitle = await _context.JobTitles.FindAsync(id);
            if (jobTitle != null)
            {
                _context.JobTitles.Remove(jobTitle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checking if logged in user is an Administrator and logged in
        /// </summary>
        /// <returns></returns>
        private bool AdminUserIsLoggedIn()
        {
            if (CurrentUser.IsLoggedIn() && CurrentUser.GetLoggedInUser.IsAdmin)
                return true;

            return false;
        }

        private bool JobTitleExists(int id)
        {
          return (_context.JobTitles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
