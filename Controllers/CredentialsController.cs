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
    public class CredentialsController : Controller
    {
        private readonly EMSDbContext _context;

        public CredentialsController(EMSDbContext context)
        {
            _context = context;
        }

        // GET: Credentials
        public async Task<IActionResult> Index()
        {
              return _context.Credentials != null ? 
                          View(await _context.Credentials.ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.Credentials'  is null.");
        }

        // GET: Credentials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (credentials == null)
            {
                return NotFound();
            }

            return View(credentials);
        }

        // GET: Credentials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserEmail,EncPass,SaltStart,SaltEnd")] Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(credentials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(credentials);
        }

        // GET: Credentials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials.FindAsync(id);
            if (credentials == null)
            {
                return NotFound();
            }
            return View(credentials);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserEmail,EncPass,SaltStart,SaltEnd")] Credentials credentials)
        {
            if (id != credentials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialsExists(credentials.Id))
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
            return View(credentials);
        }

        // GET: Credentials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (credentials == null)
            {
                return NotFound();
            }

            return View(credentials);
        }

        // POST: Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Credentials == null)
            {
                return Problem("Entity set 'EMSDbContext.Credentials'  is null.");
            }
            var credentials = await _context.Credentials.FindAsync(id);
            if (credentials != null)
            {
                _context.Credentials.Remove(credentials);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialsExists(int id)
        {
          return (_context.Credentials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
