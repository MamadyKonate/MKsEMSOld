using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Credentials
    {
        [Key]
        public int Id { get; set; }
        public string userEmail { get; set; }
        public string encPass { get; set; }
        public string saltStart { get; set; }
        public string saltEnd { get; set; }
    }
}
