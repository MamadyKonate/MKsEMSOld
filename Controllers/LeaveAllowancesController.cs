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
    public class LeaveAllowancesController : Controller
    {
        private readonly EMSDbContext _context;

        public LeaveAllowancesController(EMSDbContext context)
        {
            _context = context;
        }

        // GET: LeaveAllowances
        public async Task<IActionResult> Index()
        {
              return _context.LeaveAllowances != null ? 
                          View(await _context.LeaveAllowances.ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.LeaveAllowances'  is null.");
        }

        // GET: LeaveAllowances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveAllowances == null)
            {
                return NotFound();
            }

            var leaveAllowance = await _context.LeaveAllowances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveAllowance == null)
            {
                return NotFound();
            }

            return View(leaveAllowance);
        }

        // GET: LeaveAllowances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllowances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Allowance")] LeaveAllowance leaveAllowance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveAllowance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveAllowance);
        }

        // GET: LeaveAllowances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveAllowances == null)
            {
                return NotFound();
            }

            var leaveAllowance = await _context.LeaveAllowances.FindAsync(id);
            if (leaveAllowance == null)
            {
                return NotFound();
            }
            return View(leaveAllowance);
        }

        // POST: LeaveAllowances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Allowance")] LeaveAllowance leaveAllowance)
        {
            if (id != leaveAllowance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveAllowance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveAllowanceExists(leaveAllowance.Id))
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
            return View(leaveAllowance);
        }

        // GET: LeaveAllowances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveAllowances == null)
            {
                return NotFound();
            }

            var leaveAllowance = await _context.LeaveAllowances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveAllowance == null)
            {
                return NotFound();
            }

            return View(leaveAllowance);
        }

        // POST: LeaveAllowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveAllowances == null)
            {
                return Problem("Entity set 'EMSDbContext.LeaveAllowances'  is null.");
            }
            var leaveAllowance = await _context.LeaveAllowances.FindAsync(id);
            if (leaveAllowance != null)
            {
                _context.LeaveAllowances.Remove(leaveAllowance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveAllowanceExists(int id)
        {
          return (_context.LeaveAllowances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
