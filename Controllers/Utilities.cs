using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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
}
