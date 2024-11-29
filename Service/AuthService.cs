using DentalDataBAse.DTO;
using DentalDataBAse.IService;
using DentalDataBAse.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static DentalDataBAse.DTO.Responses;

namespace DentalDataBAse.Service
{
    public class AuthService : IAuths

    {
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        public async Task<GeneralResponse> CreateAccount(RegisterDTO userDTO, bool isAdmin = false)
        {
            if (userDTO == null)
            {
                return new GeneralResponse(false, "Model is Empty");
            }

            var newUser = new ApplicationUsers()
            {
                Email = userDTO.Email,
                FullName = userDTO.FullName,
                PasswordHash = userDTO.password,
                UserName = userDTO.Email,
                PhoneNumber = userDTO.phoneNumber,
                HomeAddress = userDTO.HomeAddress
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                return new GeneralResponse(false, "The Email Already Exists");
            }

            var createUser = await userManager.CreateAsync(newUser, userDTO.password);
            if (!createUser.Succeeded)
            {
                return new GeneralResponse(false, "An Error Occurred");
            }

            // Check if the "Admin" role exists, and create it if necessary
            var checkAdminRole = await roleManager.FindByNameAsync("Admin");
            if (checkAdminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            // Check if the "User" role exists, and create it if necessary
            var checkUserRole = await roleManager.FindByNameAsync("User");
            if (checkUserRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }

            // Assign the "Admin" role if isAdmin is true; otherwise, assign the "User" role
            if (isAdmin)
            {
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Admin account created successfully.");
            }
            else
            {
                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "User account created successfully.");
            }
        }

        public async Task<List<UserDTO>> ListAllUsers()
        {
            var users =  userManager.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                HomeAddress = user.HomeAddress
            }).ToList();

            return users;
        }

        public async Task<GeneralResponse> DeleteUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new GeneralResponse(false, "User not found.");
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new GeneralResponse(false, "An error occurred while deleting the user.");
            }

            return new GeneralResponse(true, "User deleted successfully.");
        }


        public Task<GeneralResponse> CreateAccount(RegisterDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return new LoginResponse(false, null, "The Login container is empty", null);
            }

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser == null)
            {
                return new LoginResponse(false, null, "The email is Incorrect/User not found", null);
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!isPasswordCorrect)
            {
                return new LoginResponse(false, null, "Invalid Email/Password", null);
            }

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUser.PhoneNumber, getUser.HomeAddress, getUserRole.First());

            string token = GenerateToken(userSession);

            // Return the full name along with the token
            return new LoginResponse(true, token, "Login successful", getUser.FullName);
        }



        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id),
                new Claim (ClaimTypes.Name, user.FullName),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.MobilePhone, user.phoneNumber),
                new Claim (ClaimTypes.StreetAddress, user.homeAddress),
                new Claim (ClaimTypes.Role, user.Role),

            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
