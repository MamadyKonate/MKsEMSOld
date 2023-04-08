using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string AddressLine1   { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string UserEmail { get; set; }
    }
}
