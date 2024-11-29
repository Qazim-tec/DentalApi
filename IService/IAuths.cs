using DentalDataBAse.DTO;
using DentalDataBAse.Model;
using static DentalDataBAse.DTO.Responses;

namespace DentalDataBAse.IService
{
    public interface IAuths
    {
        Task<GeneralResponse> CreateAccount(RegisterDTO userDTO, bool isAdmin);

        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);

        Task<List<UserDTO>> ListAllUsers();
        Task<GeneralResponse> DeleteUser(string email);



    }
}
