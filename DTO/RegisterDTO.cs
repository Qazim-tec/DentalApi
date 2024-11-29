using System.ComponentModel.DataAnnotations;

namespace DentalDataBAse.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string FullName {  get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(password))]
        public string connfirmedPasswored { get; set; }
        public string phoneNumber { get; set; }
        public string HomeAddress { get; set; }
    }
}
