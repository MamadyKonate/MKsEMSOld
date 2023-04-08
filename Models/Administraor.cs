namespace MKsEMS.Models
{
    public class Administraor : User
    {
        //these should probably go in the Controllers namespace
        public void CreateUser(User user)
        {
            
        }
        public User GetEmployeeDetail(string email)
        {
            return new User();         
        }
    }
}
