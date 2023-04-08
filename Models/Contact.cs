using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string AddressLine1   { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string UserEmail { get; set; }
    }
}
