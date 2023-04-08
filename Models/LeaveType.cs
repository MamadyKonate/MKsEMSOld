using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
    }
}
