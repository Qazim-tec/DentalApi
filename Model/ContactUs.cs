using System.ComponentModel.DataAnnotations;

namespace DentalDataBAse.Model
{
    public class ContactUs
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Complain { get; set; }

    }
}
