using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DentalDataBAse.Model
{
    public class ApplicationUsers : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(300)]
        public string HomeAddress {  get; set; }


    }
}
