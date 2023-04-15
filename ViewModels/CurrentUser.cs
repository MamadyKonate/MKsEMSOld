using MKsEMS.Models;

namespace MKsEMS.ViewModels
{
    public class CurrentUser
    {
        public User GetLoggedInUser { get; set; } = new();
        public bool IsLoggedIn()
        {
            if (GetLoggedInUser.IsUserLoggedIn)
                return true;

            return false;
        }
    }
}
