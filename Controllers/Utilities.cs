
using Microsoft.AspNetCore.Mvc.Rendering;
using MKsEMS.Data;
using MKsEMS.Models;

namespace MKsEMS.Controllers
{
    public static class CurrentUser 
    {
        public static User GetLoggedInUser { get; set; } = new();

        /// <summary>
        /// Returns true if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public static bool IsLoggedIn()
        {
            if (GetLoggedInUser.IsUserLoggedIn)
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
        public static List<Leave>? GetBookings() { return alldrop.GetBookings(); }
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
        /// an admin user is creating/updating Booking for a member
        /// </summary>
        /// <returns>List emails belonging to Users</returns>
        public List<string>? GetUsersEmails()
        {
            List<string> _emails = new();

            if (_context.Users != null)
                foreach (var item in _context.Users)
                {
                    _emails.Add(item.Email);
                }

            return _emails;
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
            var user = GetUsers().Where(u => u.Email == email).First();
            return user.Id;
        }
        //*******************************************************************************
        /// <summary>
        /// Retrieving all the all LeaveTypes (name) from the database.
        /// </summary>
        /// <returns>List of string LeaveTypes</returns>
        public List<string>? GetLeaveTypes()
        {
            List<string> allLeaveTypes = new();
            if (_context.LeaveTypes != null)
                foreach (var n in _context.LeaveTypes)
                {
                    allLeaveTypes.Add(n.Name.ToString());
                }
            return allLeaveTypes;
        }

        /// <summary>
        /// Retrieving from the Sqlite DB, all the Tee time bookins/reservation made from the database.
        /// </summary>
        /// <returns>List Booking objects</returns>
        public List<Leave> GetBookings()
        {
            if (_context.Leaves != null)
            {
                return _context.Leaves.ToList();
            }

            return null;
        }        
        
        
    }

}
