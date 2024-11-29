using DentalDataBAse.IService;
using DentalDataBAse.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalDataBAse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointment;

        public AppointmentController(IAppointment appointment)
        {
            _appointment = appointment;
        }

        [HttpPost]
        [Route("NewAppointment")]
        public IActionResult createAppointment([FromBody] Appointment appointment)
        {
            _appointment.createAppointment(appointment);
            return Ok("Appointment successfully submited");


        }



        [HttpDelete]
        [Route("DeleteAppointmentByID{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var getAppointment = _appointment.getAppointmentbyId(id);
            if (getAppointment == null)
            {
                return NotFound("Id not found");

            }

            _appointment.deleteAppointment(getAppointment);
            return Ok("Appointment successfully Deleted");

        }
        
        [HttpGet]
        [Route("GetAllAppointment")]
        public IActionResult GetAllAppointment()
        {
            List<Appointment> appointment = _appointment.getAppointmentList();
            return Ok(appointment);
        }



    }
}
