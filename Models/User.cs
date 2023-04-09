using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string SurName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, EmailAddress]
        public int ManagerEmail { get; set; }
        [Required]
        public string Department { get; set; }
        [Required, DataType(DataType.Date)]
        public DateOnly DOB { get; set; }
        public double LeaveEntitement { get; set; } = 25;
        public double LeaveTaken { get; set; }
        public double SickLeaveTaken { get; set; }
        [Required]
        public double Salary { get; set; }
        public bool IsUserLoggedIn { get; set; }

    }
}
