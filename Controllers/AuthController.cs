using DentalDataBAse.DTO;
using DentalDataBAse.IService;
using DentalDataBAse.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalDataBAse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuths _auths;

        public AuthController(IAuths auths)
        {
            _auths = auths;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDTO addUser)
        {
            if (addUser == null)
            {
                return BadRequest("User data is required");
            }

            // Create a regular user (not an admin)
            var response = await _auths.CreateAccount(addUser, isAdmin: false);
            return Ok(response);
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] RegisterDTO addUser)
        {
            if (addUser == null)
            {
                return BadRequest("User data is required");
            }

            // Create an admin user
            var response = await _auths.CreateAccount(addUser, isAdmin: true);
            return Ok(response);
        }

        [HttpPost]
        [Route("LoginIn")]
        public async Task<IActionResult> LoginIn([FromBody] LoginDTO login)
        {
            var response = await _auths.LoginAccount(login);
            return Ok(response);

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            List<UserDTO> UserList =  await _auths.ListAllUsers();
            return Ok(UserList);
        }


        [HttpDelete("DeleteUser/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var response = await _auths.DeleteUser(email)   ;
            if (!response.flag)
            {
                return BadRequest(new { message = response.Message });
            }

            return Ok(new { message = response.Message });
        }


    }
}
