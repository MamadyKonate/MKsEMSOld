using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MKsEMS.Data;
using MKsEMS.Models;
using MKsEMS.Services;

namespace MKsEMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly EMSDbContext _context;
        private Credentials _credentials = new();
        private readonly CurrentUser2 _currentUser ;

        public UsersController(EMSDbContext context, CurrentUser2 currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Users
        /// <summary>
        /// Getting Users for Administrators, Managers and CEO only
        /// </summary>
        /// <returns>List of Users</returns>
        public async Task<IActionResult> Index()
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
                        
            if(!_currentUser.GetLoggedInUser().IsAdmin &&
               !_currentUser.GetLoggedInUser().IsManager &&
               !_currentUser.GetLoggedInUser().IsCEO)
                return RedirectToAction("Index", "Leaves"); //User is logged in with least privilege

            return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'EMSDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        //Any logged in user should be able to get at least his/her own detail
        public async Task<IActionResult> Details(int? id)
        {
            if (!_currentUser.IsLoggedIn())
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
                        
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        /// <summary>
        /// Only Administrators are allowed to create User account
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            TempData["AdminMessage"] = "";

            if (!AdminUserIsLoggedIn())
            {
                TempData["AdminMessage"] = "Please login as an Administrator";
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            }

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,Email,JobTitle,ManagerEmail,DOB,LeaveEntitement,LeaveTaken,SickLeaveTaken,IsUserLoggedIn,IsAdmin,IsManager,IsCEO")] User user)
        {
            TempData["AdminMessage"] = "";

            TempData["CreateMessageFail"] = "";            

            if (!AdminUserIsLoggedIn())
            {
                TempData["AdminMessage"] = "Please login as an Administrator";
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            }

            if (ModelState.IsValid)
            {
                //creating email address for the user
                SetEmail(user);
                string tempPass = GenerateRandomPass.GeTempPassword();
                //Adding/creating email and temporary password into Credentials table for the user                 
                _credentials.UserEmail = user.Email;                     
                _credentials.EncPass = EncDecPassword.Enc64bitsPass(tempPass);

                user.FirstTimeLogin = true; //So the user will be forced to change password on first login

                await _context.AddAsync(_credentials);
                _context.SaveChangesAsync();
                
                //now creating a record in Users table for the user
                _context.Add(user);                
                await _context.SaveChangesAsync();

                TempData["CreateMessageSuccess"] = "User: " + user.Email + " - Password: " + tempPass;
                TempData["userEmail"] = user.Email;
                return RedirectToAction("Create", "Contacts");
            }
            TempData["CreateMessageFail"] = "The user could not be created at this time. Please try again later.";
            return View(user);
        }

        /// <summary>
        /// Creating email address for the new user.
        /// It is done based on [Firstname].[Surname]@[DomainName]
        /// If there is another user with the same full name (same email signature),
        /// then, a digit number is added to Surename. i.e.: joe.smith1@domain.ie
        /// </summary>
        /// <param name="user">The new user the email is being created for</param>
        private void SetEmail(User user)
        {
            int counter = 0;
            string email, domainName = (_context.Companies.First().DomainName);
            bool emailSet = false;
            do
            {
                if (counter == 0)
                {
                    email = string.Concat(user.FirstName, ".", user.Surname, "@", domainName);
                }
                else
                {
                    email = string.Concat(user.FirstName, ".", user.Surname, counter, "@", domainName);
                }
                var tempUser = _context.Users.Where(u => u.Email.ToLower() == email.ToLower());

                if (tempUser.IsNullOrEmpty())
                    emailSet = true;
                    user.Email = email;

                counter++;

            } while (!emailSet);

        }


        // GET: Users/Edit/5
        /// <summary>
        /// Opening the form for editing an existing User account 
        /// </summary>
        /// <param name="id">Id of the selected user to edit</param>
        /// <returns>View page of user to edit </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["AdminMessage"] = "";

            if (!AdminUserIsLoggedIn())
            {
                TempData["AdminMessage"] = "Please login as an Administrator";
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            }

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Editing the user account into the database assuming all createria are met,
        /// otherwise a View is returned with the user information in for correction
        /// </summary>
        /// <param name="id">Id of the selected user</param>
        /// <param name="user">Selected user</param>
        /// <returns>Index page of list of users </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,Email,JobTitle,ManagerEmail,DOB,LeaveEntitement,LeaveTaken,SickLeaveTaken,IsUserLoggedIn,IsAdmin,IsManager,IsCEO")] User user)
        {
            TempData["AdminMessage"] = "";

            if (!AdminUserIsLoggedIn())
            {
                TempData["AdminMessage"] = "Please login as an Administrator";
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            }

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

       
        // GET: Users/Delete/5
        ///<summary>
        /// Opening the form for deleting an existing User account
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <returns>User to be deleted</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["AdminMessage"] = "";

            if (!AdminUserIsLoggedIn())
            {
                TempData["AdminMessage"] = "Please login as an Administrator";
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            }

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        /// <summary>
        /// Deleting the user account from the database
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <returns>Redirected to list of users</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["AdminMessage"] = "";

            if (!AdminUserIsLoggedIn())
            {
                TempData["AdminMessage"] = "Please login as an Administrator";
                return RedirectToAction("Index", "UserLogins"); //Only if user is not already logged in;
            }

            if (_context.Users == null)
            {
                return Problem("Entity set 'EMSDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            var userCredentials =  _context.Credentials.Where(uc => uc.UserEmail == user.Email).FirstOrDefault();
            if (user != null)
            {
                _context.Users.Remove(user);

                RemoveContact(user.Email);  //will only remove associated email if it exist

                if(userCredentials !=null)
                    _context.Credentials.Remove(userCredentials);
            }
            


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        /// Checking if current user is logged in and if s/he is an Administrator
        /// </summary>
        /// <returns>true or false</returns>
        private bool AdminUserIsLoggedIn()
        {
            if (_currentUser.GetLoggedInUser() == null)
                return false;

            return _currentUser.IsLoggedIn() && _currentUser.GetLoggedInUser().IsAdmin;

            //if (_currentUser.IsLoggedIn() && _currentUser.GetLoggedInUser().IsAdmin)
            //    return true;

            //return false;
        }

        private void RemoveContact(string email) 
        {
            Contact contact = _context.Contacts.Where(c => c.UserEmail == email).FirstOrDefault();

            if (contact != null)
                _context.Remove(contact);
        }


        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
