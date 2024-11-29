using System.ComponentModel.DataAnnotations;

namespace DentalDataBAse.DTO
{
  
    
        public record UserSession(string? Id, string? FullName, string? Email, string? Role, string homeAddress, string phoneNumber);

    
}
