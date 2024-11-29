using DentalDataBAse.Model;

namespace DentalDataBAse.IService
{
    public interface IhomeService
    {
        void CreateHomeService(HomeService homeService);
        void DeleteHomeService(HomeService homeService);
        List<HomeService> GetAllHomeService();
        HomeService GetHomeServiceById(int id);
        HomeService GetHomeServiceByName(string name);

   
    }
}
