using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        [Required, EmailAddress]
        public string UserEmail { get; set; }
        [Required, EmailAddress]
        public string ManagerEmail { get; set; }
        [Required, DataType(DataType.Date)]
        public DateOnly DateFrom { get; set; }
        [Required, DataType(DataType.Date)]
        public DateOnly DateTo { get; set; }
        [Required]
        public int Allowance { get; set; }
        public int? numberOfDays { get; set; }
        public int Taken { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public bool LeaveStatus { get; set; }
        public string? DenialReason { get; set; }
    }
}
