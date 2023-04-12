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
    public class CompaniesController : Controller
    {
        private readonly EMSDbContext _context;

        public CompaniesController(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            return _context.Companies != null ?
                          View(await _context.Companies.ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.Companies'  is null.");
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                if (!GrantedAccess())
                    return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,LogoURI,IsToBeDeleted")] Company company)
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

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
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,LogoURI,IsToBeDeleted")] Company company)
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        /// <summary>
        /// This method is called when the user clicks on the Delete button on Company.
        /// As this is an important account, it has to be confirmed twice.
        /// It's only deleted after the second delete, otherwise it's marked as deleted, but not actually deleted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!GrantedAccess())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (_context.Companies == null)
            {
                return Problem("Entity set 'EMSDbContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                if (company.IsToBeDeleted)
                {
                    _context.Companies.Remove(company);
                }
                else
                {
                    company.IsToBeDeleted = true;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if the current user should have access to Company details
        /// Only CEO and Admins should have access
        /// </summary>
        /// <returns></returns>
        private bool GrantedAccess()
        {
            if (!CurrentUser.IsLoggedIn()
                && (!CurrentUser.GetLoggedInUser.IsCEO || !CurrentUser.GetLoggedInUser.IsAdmin))
                return false;

            return true;
        }
        private bool CompanyExists(int id)
        {
          return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
