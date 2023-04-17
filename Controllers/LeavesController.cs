using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MKsEMS.Controllers
{
    public class LeavesController : Controller
    {
        private readonly EMSDbContext _context;
        private  CurrentUser2 _currentUser;
        private DateTime _startDate = new(), _endDate = new();
        TimeSpan duration;
        int daysOff;

        public LeavesController(EMSDbContext context, CurrentUser2 currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }        
        
        public bool EnoughDayToTake()
        {
            double remainingDays = _currentUser.GetLoggedInUser().LeaveEntitement - _currentUser.GetLoggedInUser().LeaveTaken;

            duration = _endDate - _startDate;

            daysOff = duration.Days +1;


            return remainingDays >= daysOff;
        }

        // GET: Leaves1
        public async Task<IActionResult> Index()
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
                        
            if (_context.Credentials != null)
            {
                return _currentUser.GetLoggedInUser().IsAdmin ?
                        View(await _context.Leaves.ToListAsync()) :

                        _currentUser.GetLoggedInUser().IsManager ?
                        View(await _context.Leaves.Where(l => l.ManagerEmail.Equals(_currentUser.GetLoggedInUser().Email)).ToListAsync()) :

                        View(await _context.Leaves.Where(l => l.UserEmail.Equals(_currentUser.GetLoggedInUser().Email)).ToListAsync());
            }

            return Problem("Entity set 'EMSDbContext.Leaves'  is null.");
        }

        // GET: Leaves1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            
            if (id == null || _context.Leaves == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // GET: Leaves1/Create
        public IActionResult Create()
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            return View();
        }

        // POST: Leaves1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserEmail,ManagerEmail,DateFrom,DateTo,Allowance,Taken,LeaveType,LeaveStatus,DenialReason")] Leave leave)
        {
            TempData["LeaveRqMsg"] = "";

            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (ModelState.IsValid)
            {
                _startDate = leave.DateFrom.ToDateTime(new TimeOnly());
                _endDate = leave.DateTo.ToDateTime(new TimeOnly()); 

                if (EnoughDayToTake())
                {
                    //Chekcing valid start date 
                    
                    if (_startDate.CompareTo(DateTime.Now.Date)>= 0 && _endDate.CompareTo(DateTime.Now.Date) >= 0) 
                    {                      
                    
                        _currentUser.GetLoggedInUser().LeaveTaken += daysOff;
                        leave.LeaveStatus = false;
                        leave.numberOfDays = daysOff;

                        _context.Add(leave);
                        _context.Users.Update(_currentUser.GetLoggedInUser());

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));                    
                    
                    }                   
                   
                    TempData["LeaveRqMsg"] = "Leave start date or end date cannot be in the past!  Please tray again";
                    return View(leave);                    
                }
                else
                {
                    TempData["LeaveRqMsg"] = "You don't seem to have enough days to book.";
                }
                
            }
            else
            {
                TempData["LeaveRqMsg"] = "Somthing went wrong, please try again";

                return View(leave);
            }
               
            return View(leave);
        }

        // GET: Leaves1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id == null || _context.Leaves == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }

        // POST: Leaves1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserEmail,ManagerEmail,DateFrom,DateTo,Allowance,Taken,LeaveType,LeaveStatus,DenialReason")] Leave leave)
        {
            _startDate = Convert.ToDateTime(leave.DateFrom);
            _endDate = Convert.ToDateTime(leave.DateTo);

            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id != leave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.Id))
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
            return View(leave);
        }

        // GET: Leaves1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (id == null || _context.Leaves == null)
            {
                return NotFound();
            }

            var leave = await _context.Leaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // POST: Leaves1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            if (_context.Leaves == null)
            {
                return Problem("Entity set 'EMSDbContext.Leaves'  is null.");
            }
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                _context.Leaves.Remove(leave);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(int id)
        {
          return (_context.Leaves?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
