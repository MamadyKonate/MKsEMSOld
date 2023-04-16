
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MKsEMS.Data;
using MKsEMS.Models;
using Newtonsoft.Json;

namespace MKsEMS.Controllers
{
    public static class CurrentUser 
    {
        public static User GetLoggedInUser { get; set; } = new();

        /// <summary>
        /// Returns true if a _user is logged in.
        /// </summary>
        /// <returns></returns>
        public static bool IsLoggedIn()
        {
            if (GetLoggedInUser.IsUserLoggedIn)
                return true;

            return false;
        }
    }

    public  class CurrentUser2
    {
        private readonly IHttpContextAccessor _sessionContext;
        private User _loggedInUser { get; set; } = new();
        public CurrentUser2(IHttpContextAccessor sessionContext)
        {
            _sessionContext = sessionContext;
        }

        // NOT SURE ANY LONGER WHY USING SESSION IN THIS AFTER ALL.
        // NOT SURE IF IT MIGHT COME HANDY IN THE LONG RUN - REMAINS TO BE SEEN
        
       
        /// <summary>
        /// Converts the _user object to a Json string and stores it in the session.
        /// </summary>
        /// <param name="user">User Currently logged in</param>
        public void SetLoggedInUser(User user)
        {
            int loggedIn = 0;

            string userJSon = JsonConvert.SerializeObject(user);
            _sessionContext.HttpContext.Session.SetString("_loggedInUser", userJSon);
            
            //the idea is that 
            if (GetLoggedInUser().IsUserLoggedIn)
                loggedIn = 1;
                
            _sessionContext.HttpContext.Session.SetString("userEmail", user.Email);               
            _sessionContext.HttpContext.Session.SetInt32("loggedInUserInt", loggedIn);

        }

        /// <summary>
        /// Reconstruct string in Json format from the session back to the User object for processing
        /// </summary>
        /// <returns>Reconstructed logged in User</returns>

        public User GetLoggedInUser ()
        {
            string userJSon = _sessionContext.HttpContext.Session.GetString("_loggedInUser");
            _loggedInUser = JsonConvert.DeserializeObject<User>(userJSon);

            return _loggedInUser;
        }

        /// <summary>
        /// 
        /// It will then return true if a _user is logged in.
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {  
            if (GetLoggedInUser().IsUserLoggedIn)
                return true;

            return false;
        }       
    }

    public static class ViewModelData
    {
        /// <summary>
        /// This is the data that is used to populate the drop down lists.
        /// </summary>
        static AllDropDownListData alldrop { get; } = new AllDropDownListData(new MKsEMS.Data.EMSDbContext());
        /// <summary>
        /// This method returns a list of bookings stored in the Sqlite databe at any given moment.
        /// </summary>
        /// <returns>A List<Booking> of all bookings</returns>
        public static List<Leave>? GetBookings() { return alldrop.GeLeaves(); }
        /// <summary>
        /// This method returns a list of users stored in the Sqlite databe at any given moment.
        /// </summary>
        /// <returns>A list of all users</returns>
        public static List<User>? GetUsers() { return alldrop.GetUsers(); }
        /// <summary>
        /// This method returns a list of specific (filtered) users stored in the Sqlite databe at any given moment.
        /// </summary>
        public static List<User>? GetFilteredUsers { get; set; } = new();
        /// <summary>
        /// The following methods return various data as SelectLists to be displayed in the views.
        /// They are all stored in the Sqlite database.
        /// </summary>
        /// <returns></returns>
        public static SelectList? GetUsersEmailsSelectList() { return new SelectList(alldrop.GetUsersEmails()); }
        public static SelectList? GetLeaveTypesSelectList() { return new SelectList(alldrop.GetLeaveTypes()); }
        public static SelectList? GetJobTitlesSelectList() { return new SelectList(alldrop.GetJobTitles()); }
        public static SelectList? GetUsersManagerEmailsSelectList() { return new SelectList(alldrop.GetUsersManagerEmails()); }
        public static SelectList? GeGetLeaveAllowancesSelectList() { return new SelectList(alldrop.GetLeaveAllowances()); }

    }


    public class AllDropDownListData
    {
        private EMSDbContext? _context { get; set; } = new();

        /// <summary>
        /// Controller setting up the database context for retrieving from
        /// and saving changes to records into the database
        /// </summary>
        /// <param name="context">Database contex</param>
        public AllDropDownListData(EMSDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieving all data from the database.
        /// </summary>
        /// <returns>List of User object</returns>
        public List<User>? GetUsers()
        {
            if (_context.Users != null)
                return _context.Users.ToList();
            return null;
        }
        /// <summary>
        /// Retrieving all emails for populating the email field when 
        /// an admin _user is creating/updating User account
        /// </summary>
        /// <returns>List emails belonging to Users</returns>
        public List<string>? GetUsersEmails()
        {
            List<string> _emails = new();

            if (_context.Users != null)
                foreach (var item in _context.Users.ToList())
                {
                    _emails.Add(item.Email);
                }

            return _emails;
        }
        /// <summary>
        /// Retrieving all emails for populating the ManagerEmail field when 
        /// an admin _user is creating/updating User accounts
        /// </summary>
        /// <returns>List emails belonging to Users</returns>
        public List<string>? GetUsersManagerEmails()
        {
            List<string> _emails = new();

            if (_context.Users != null)
            {  
                foreach (var n in _context.Users.Where(u => u.IsManager).ToList())
                {
                    _emails.Add(n.Email);
                }              
            }               

            return _emails;
        }

        /// <summary>
        /// Retrieving all LeaveAllowances for populating the LeaveEntitlement field when 
        /// an admin _user is creating/updating User accounts
        /// </summary>
        /// <returns>List LeaveAllowances</returns>
        public List<string>? GetLeaveAllowances()
        {
            List<string> _leaveAllowance = new();

            if (_context.LeaveAllowances != null)
            {
                foreach (var la in _context.LeaveAllowances)
                {
                    _leaveAllowance.Add(la.Allowance.ToString());
                }
            }

            return _leaveAllowance;
        }

        //*****************************************************************************
        /// <summary>
        /// To be removed - no referrence to it
        /// It's no longer needed
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetIdByEmail(string email)
        {
            var _user = GetUsers().Where(u => u.Email == email).First();
            return _user.Id;
        }
        //*******************************************************************************
        /// <summary>
        /// Retrieving all the all LeaveTypes (name) from the database.
        /// </summary>
        /// <returns>List of string LeaveTypes</returns>
        public List<string>? GetLeaveTypes()
        {
            List<string> _allLeaveTypes = new();
            if (_context.LeaveTypes != null)
                foreach (var n in _context.LeaveTypes)
                {
                    _allLeaveTypes.Add(n.Name);
                }
            return _allLeaveTypes;
        }

        public List<string>? GetJobTitles()
        {
            List<string> _allJobTitles = new();
            if (_context.Jobs != null)
                foreach (var t in _context.Jobs)
                {
                    _allJobTitles.Add(t.JobTitle);
                }
            return _allJobTitles;
        }

        /// <summary>
        /// Retrieving from the Sqlite DB, all Leave bookings made from the database.
        /// </summary>
        /// <returns>List Booking objects</returns>
        public List<Leave> GeLeaves()
        {
            if (_context.Leaves != null)
            {
                return _context.Leaves.ToList();
            }

            return null;
        }        
        
        
    }

}
