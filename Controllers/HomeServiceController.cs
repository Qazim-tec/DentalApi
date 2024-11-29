using DentalDataBAse.IService;
using DentalDataBAse.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalDataBAse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeServiceController : ControllerBase
    {
        private readonly IhomeService _homeService;

        public HomeServiceController(IhomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpPost]
        [Route("NewHomeService")]
        public IActionResult CreateHomeServices([FromBody] HomeService homeService)
        {
            _homeService.CreateHomeService(homeService);
            return Ok("Home Service successfully Added");
        }


        [HttpDelete]
        [Route("DeleteHomeServiveById{id}")]
        public IActionResult DeleteHomeServices(int id)
        {
            var getHomeService = _homeService.GetHomeServiceById(id);
            if (getHomeService == null)
            {
                return NotFound("ID not Found");
            }
            _homeService.DeleteHomeService(getHomeService);
            return Ok("Home Service successfully Deleted");
        }

        [HttpGet]
        [Route("GetAllHomeService")]
        public IActionResult GetHomeServices()
        {
            List<HomeService> homeServices = _homeService.GetAllHomeService();
            return Ok(homeServices);

        }
       

    }
}
