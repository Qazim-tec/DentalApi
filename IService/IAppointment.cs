using DentalDataBAse.Model;

namespace DentalDataBAse.IService
{
    public interface IAppointment
    {
        void createAppointment(Appointment appointment);

        List<Appointment> getAppointmentList();

        Appointment getAppointmentbyId(int id);
        
        void deleteAppointment(Appointment appointment);

        Appointment getAppointmentbyName(String Name); 
        


    }
}
