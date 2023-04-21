using Microsoft.AspNetCore.Mvc;
using MKsEMS.Data;
using MKsEMS.Models;
using MKsEMS.Services;

namespace MKsEMS.Controllers
{
    public class UserLoginsController : Controller
    {
        private readonly EMSDbContext _context;
        private CurrentUser2 _loggedInUser;
        private readonly AllDropDownListData _allDropData;
        private static int _incorrectPasswordEntered;


        public UserLoginsController(EMSDbContext context, CurrentUser2 loggedInUser)
        {
            _context = context;
            _loggedInUser = loggedInUser;
            _allDropData = new (context);
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
                if (_loggedInUser.GetLoggedInUser() != null)                    
                _loggedInUser.GetLoggedInUser().IsUserLoggedIn = false;   
                TempData["Message"] = "Please login to continue";
            }
            catch { return View(); }

            return View();
        }
        /// <summary>
        /// Comparing user's _credentials their conterpart on the DB
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
                TempData["email"] = "";

                //Getting account with matching email first
                var userCredentials = (_context.Credentials.Where(
                    uc => uc.UserEmail == user.Email)).FirstOrDefault();

                if (userCredentials != null)  //we have our user
                {
                    //Now we decode userCredentials's password on the system
                    var pass = EncDecPassword.DecodeFrom64(userCredentials.EncPass);

                    //then we compare the passwords
                    if (pass == user.Password)
                    {
                        //we then configure the logged-in user accordingly
                        var theUser = (_context.Users.Where(us => us.Email == userCredentials.UserEmail)).FirstOrDefault();

                        theUser.IsUserLoggedIn = true;

                        //Putting the logged in user in the session in CurrentUser2 class
                        _loggedInUser.SetLoggedInUser(theUser);

                        _incorrectPasswordEntered = 0;                        
                        
                        _allDropData.GetFilteredUsers = ViewModelData.GetUsers();

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
                        _loggedInUser.GetLoggedInUser().IsUserLoggedIn = false;                        
                        TempData["Message"] = "Incorrect Username or Password entered " + (_incorrectPasswordEntered += 1) + " time(s).";
                        return View();
                    }

                }
                else //no user found in the system with the same email as entered
                {
                    _loggedInUser.GetLoggedInUser().IsUserLoggedIn = false;
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
            _loggedInUser.GetLoggedInUser().IsUserLoggedIn = false;            
            return View("Index");
        }

       
        private bool UserLoginExists(int id)
        {
            return (_context.UserLogins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
