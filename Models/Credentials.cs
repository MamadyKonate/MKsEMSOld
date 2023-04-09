using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Credentials
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string userEmail { get; set; }
        [Required]
        public string encPass { get; set; }
        [Required]
        public string saltStart { get; set; }
        [Required]
        public string saltEnd { get; set; }
    }
}
