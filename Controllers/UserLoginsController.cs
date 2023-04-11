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
    public class UserLoginsController : Controller
    {
        private readonly EMSDbContext _context;
        private static int _incorrectPasswordEntered;
       

        public UserLoginsController(EMSDbContext context)
        {
            _context = context;            
        }

        // GET: UserLogins

        /// <summary>
        /// Getting the login page
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            //Ensure Current user is not set to IsLoggedIn - in case the HTTP Post is comming from Log out
            try
            {
                CurrentUser.GetLoggedInUser.IsUserLoggedIn = false;
                TempData["Message"] = "Please login to continue";
            }
            catch { return View(); }

            return View();
        }
        /// <summary>
        /// Comparing user's credentials their conterpart on the DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: UserLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] UserLogin user)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "";
              
                var userLogin = _context.Credentials.Where(
                    u => u.UserEmail == user.Email && u.EncPass == user.Password).FirstOrDefault();

                if (userLogin != null)  //we have our user
                {
                    var theUser = _context.Users.Where(us => us.Email == userLogin.UserEmail).FirstOrDefault();

                    theUser.IsUserLoggedIn = true;

                    CurrentUser.GetLoggedInUser = theUser;
                    _incorrectPasswordEntered = 0;

                    //                 ViewModelData.GetFilteredUsers = ViewModelData.GetUsers();

                    if (theUser.IsAdmin || theUser.IsManager)
                    {
                        return RedirectToAction("index", "Users");
                    }
                    else 
                    {
                        return RedirectToAction("index", "Leaves");
                    }
                }
                else
                {
                    CurrentUser.GetLoggedInUser.IsUserLoggedIn = false;
                    TempData["Message"] = "Incorrect Username or Password entered " + (_incorrectPasswordEntered += 1) + " time(s).";
                    return View();
                }
            }

            return View("Index");
        }

        /// <summary>
        /// Setting logged in user's IsUserLoggedIn to false and redirecting the user to login page
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            CurrentUser.GetLoggedInUser.IsUserLoggedIn = false;
            return View("Index");
        }

       
        private bool UserLoginExists(int id)
        {
            return (_context.UserLogin?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
