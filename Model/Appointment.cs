using System.ComponentModel.DataAnnotations;

namespace DentalDataBAse.Model
{
    public class Appointment
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string FullName {  get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string AppointmentDate { get; set; }
        public string PreferedTime {  get; set; }
        public string ServiceType { get; set; }


    }
}
