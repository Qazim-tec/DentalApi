using DentalDataBAse.Data;
using DentalDataBAse.IService;
using DentalDataBAse.Model;

namespace DentalDataBAse.Service
{
    public class Dental : IDentalService
    {
        private readonly PatientDbContext patientDbContext;
        public Dental(PatientDbContext patientDbContext)
        {
            this.patientDbContext = patientDbContext;
            
        }
       

        public void CreateContactUs(ContactUs contactUs)
        {
            patientDbContext.Contact.Add(contactUs);
            patientDbContext.SaveChanges();
        }

       

        public void DeleteContactUs(ContactUs contactUs)
        {
            patientDbContext.Contact.Remove(contactUs);
            patientDbContext.SaveChanges();

        }

        public List<ContactUs> GetAllContactUs()
        {
            return patientDbContext.Contact.ToList();
        }

        public ContactUs GetContactById(int id)
        {
            ContactUs contactUs = patientDbContext.Contact.Find(id);
            return contactUs;
        }

        public ContactUs GetContactByName(string name)
        {
            var contactUs = patientDbContext.Contact.Where(Ct => Ct.Name == name).FirstOrDefault();
            return contactUs;
        }
    }


}
