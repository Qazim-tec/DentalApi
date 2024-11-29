using DentalDataBAse.Data;
using DentalDataBAse.IService;
using DentalDataBAse.Model;

namespace DentalDataBAse.Service
{
    public class AppointmentService : IAppointment
    {
        private readonly PatientDbContext patientDbContext;

        public AppointmentService(PatientDbContext patientDbContext)
        {
           
            this.patientDbContext = patientDbContext;
        }

        public void createAppointment(Appointment appointment)
        {
            patientDbContext.Appointments.Add(appointment);
            patientDbContext.SaveChanges();
        }

        public void deleteAppointment(Appointment appointment)
        {
            patientDbContext.Appointments.Remove(appointment);
            patientDbContext.SaveChanges();
        }

        public Appointment getAppointmentbyId(int id)
        {

            Appointment appointment = patientDbContext.Appointments.Find(id);
            return appointment;
        }

        public Appointment getAppointmentbyName(string Name)
        {
           var  appointment = patientDbContext.Appointments.Where(ap => ap.FullName == Name).FirstOrDefault();
            return appointment;
        }

        public List<Appointment> getAppointmentList()
        {
            return patientDbContext.Appointments.ToList();
        }
    }
}
