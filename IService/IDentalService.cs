using DentalDataBAse.Model;

namespace DentalDataBAse.IService
{
    public interface IDentalService
    {
        //create ContactUs 
        void CreateContactUs(ContactUs contactUs );

        //Get all ContactUs
        List<ContactUs> GetAllContactUs();

        //Get Contact by Id
        ContactUs GetContactById(int id);

        void DeleteContactUs(ContactUs contactUs);

        //get Contact by their name
        ContactUs GetContactByName(string name);
      
    }
}
