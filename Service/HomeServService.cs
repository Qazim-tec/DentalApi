using DentalDataBAse.Data;
using DentalDataBAse.IService;
using DentalDataBAse.Model;

namespace DentalDataBAse.Service
{
    public class HomeServService : IhomeService
    {
        private readonly PatientDbContext patientDbContext;

        public HomeServService(PatientDbContext patientDbContext)
        {
            this.patientDbContext = patientDbContext;
        }
        public void CreateHomeService(HomeService homeService)
        {
           patientDbContext.HomeServices.Add(homeService);
           patientDbContext.SaveChanges();
        }

        public void DeleteHomeService(HomeService homeService)
        {
            patientDbContext.HomeServices.Remove(homeService);
            patientDbContext.SaveChanges();
        }

        public List<HomeService> GetAllHomeService()
        {
            return patientDbContext.HomeServices.ToList();
        }

        public HomeService GetHomeServiceById(int id)
        {
            HomeService homeService = patientDbContext.HomeServices.Find(id);
            return homeService;
        }
        public HomeService GetHomeServiceByName(string name)
        {
            var homeService = patientDbContext.HomeServices.Where(Hs => Hs.FullName == name).FirstOrDefault();
            return homeService;
        }
    }
}
