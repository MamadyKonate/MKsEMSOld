using Microsoft.AspNetCore.Mvc;
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

        // GET: Companies
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
        public async Task<IActionResult> Create([Bind("Id,Name,Address1,Address2,City,County,Eircode,Phone,Email,LogoURI,IsToBeDeleted")] Company company)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address1,Address2,City,County,Eircode,Phone,Email,LogoURI,IsToBeDeleted")] Company company)
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

        // POST: Companies/Delete/5
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
                _context.Companies.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
          return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
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
    }
}
