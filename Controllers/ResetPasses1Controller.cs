using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKsEMS.Data;
using MKsEMS.Services;
using MKsEMS.ViewModels;

namespace MKsEMS.Controllers
{
    public class ResetPasses1Controller : Controller
    {
        private readonly EMSDbContext _context;
        private readonly CurrentUser2 _currentUser;

        public ResetPasses1Controller(EMSDbContext context, CurrentUser2 currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        //// GET: ResetPasses1
        //public async Task<IActionResult> Index()
        //{
        //      return _context.ResetPasses != null ? 
        //                  View(await _context.ResetPasses.ToListAsync()) :
        //                  Problem("Entity set 'EMSDbContext.ResetPasses'  is null.");
        //}

        //// GET: ResetPasses1/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.ResetPasses == null)
        //    {
        //        return NotFound();
        //    }

        //    var resetPass = await _context.ResetPasses
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (resetPass == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(resetPass);
        //}

        // GET: ResetPasses1/Create
        public IActionResult Create()
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;

            return View();
        }

        // POST: ResetPasses1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,CurrentPassword,NewPassword,ReEnterNewPassword")] ResetPass resetPass)
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
                TempData["PassMessageSuccess"] = "";
                TempData["PassMessage"] = "";

            if (ModelState.IsValid)
            {                
                if (resetPass.NewPassword != resetPass.ReEnterNewPassword)
                {
                    TempData["PassMessage"] = "New passwords do not match";

                    return View(resetPass);
                }
               
                var credentials = await _context.Credentials.Where(c => c.UserEmail == resetPass.Email).FirstOrDefaultAsync();
                
                if (credentials.UserEmail != null)
                {
                    if (EncDecPassword.DecodeFrom64(credentials.EncPass) == resetPass.CurrentPassword)
                    {
                        credentials.EncPass = EncDecPassword.Enc64bitsPass(resetPass.NewPassword);
                        await _context.SaveChangesAsync();
                        TempData["PassMessageSuccess"] = "Password successfully reset";
                        return View();
                    }
                    //otherwise
                    TempData["PassMessage"] = "Incorrect password entered";
                    return View(resetPass);
                }
            }
            else
            {
                TempData["PassMessage"] = "Invalid entry, please verify";
            }

            return View(resetPass);
        }

        //// GET: ResetPasses1/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.ResetPasses == null)
        //    {
        //        return NotFound();
        //    }

        //    var resetPass = await _context.ResetPasses.FindAsync(id);
        //    if (resetPass == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(resetPass);
        //}

        //// POST: ResetPasses1/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Email,CurrentPassword,NewPassword,ReEnterNewPassword")] ResetPass resetPass)
        //{
        //    if (id != resetPass.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(resetPass);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ResetPassExists(resetPass.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(resetPass);
        //}

        //// GET: ResetPasses1/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.ResetPasses == null)
        //    {
        //        return NotFound();
        //    }

        //    var resetPass = await _context.ResetPasses
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (resetPass == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(resetPass);
        //}

        //// POST: ResetPasses1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.ResetPasses == null)
        //    {
        //        return Problem("Entity set 'EMSDbContext.ResetPasses'  is null.");
        //    }
        //    var resetPass = await _context.ResetPasses.FindAsync(id);
        //    if (resetPass != null)
        //    {
        //        _context.ResetPasses.Remove(resetPass);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ResetPassExists(int id)
        //{
        //  return (_context.ResetPasses?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
