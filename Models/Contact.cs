using System.ComponentModel.DataAnnotations;

namespace MKsEMS.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]     
        public string Address1 { get; set; } = "1234 Main St.";
        [Required]
        public string Address2 { get; set; } = "Suite 123";
        [Required]
        public string City { get; set; } = "Anytown";
        [Required]
        public string County { get; set; } = "Anycounty";
        [Required]
        public string Eircode { get; set; } = "12345";
        [Phone]
        public string Phone { get; set; } = "123-456-7890";
        [Phone]
        public string Mobile { get; set; } = "123-456-7890";
        [Required, EmailAddress]
        public string UserEmail { get; set; } = "someEmail.emailc.ie";
    }
}
