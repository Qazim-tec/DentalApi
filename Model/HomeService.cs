using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DentalDataBAse.Model
{
    public class HomeService
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }   
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string ServiceType { get; set; }
    }
}
