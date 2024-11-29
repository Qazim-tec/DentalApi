using DentalDataBAse.IService;
using DentalDataBAse.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalDataBAse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentalController : ControllerBase
    {
        private readonly IDentalService _dentalService;

        public DentalController(IDentalService dentalService)
        {
            _dentalService = dentalService;
        }

        [HttpPost]
        [Route("NewContactUs")]
        public IActionResult CreateContactUs([FromBody] ContactUs contactUs)
        {
            _dentalService.CreateContactUs(contactUs);
            return Ok("ContactUs successfully Added");


        }

        [HttpGet("GetAllcontact")]
        public IActionResult GetAllContacts()
        {
            List<ContactUs> contactUs = _dentalService.GetAllContactUs();
            return Ok(contactUs);
        }


        [HttpDelete("DeletContactById{id}")]
      
        public IActionResult DeleteContactUs(int id)
        {
            var getContact = _dentalService.GetContactById(id);
            if (getContact == null)
            {
                return NotFound("Id not found");

            }
            _dentalService.DeleteContactUs(getContact);
            return Ok("ContactUs successfully deleted");
        }


    }
}
