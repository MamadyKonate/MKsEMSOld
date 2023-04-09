using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, EmailAddress]
        public int ManagerEmail { get; set; }
        [Required]
        public string Department { get; set; }        
        public DateOnly DOB { get; set; }
        public double LeaveEntitement { get; set; } = 25;
        public double LeaveTaken { get; set; }
        public double SickLeaveTaken { get; set; }
        [Required]
        public double Salary { get; set; }

    }
}
