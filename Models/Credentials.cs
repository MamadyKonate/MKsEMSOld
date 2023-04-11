using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Credentials
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string EncPass { get; set; }
        //[Required]
        //public string SaltStart { get; set; }
        //[Required]
        //public string SaltEnd { get; set; }
        //[Required, MaxLength(2)]
        //public int Start { get; set; }
        //[Required, MaxLength(2)]
        //public int End { get; set; }
    }
}
