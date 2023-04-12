using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class JobTitle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
