using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public int ManagerId { get; set; }
        public string Department { get; set; }
        public DateOnly DOB { get; set; }
        public double LeaveEntitement { get; set; }
        public double LeaveTaken { get; set; }
        public double SickLeaveTaken { get; set; }
        public double Salary { get; set; }

    }
}
