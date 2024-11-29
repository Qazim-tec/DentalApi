using DentalDataBAse.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalDataBAse.Data
{
    public class PatientDbContext : IdentityDbContext<ApplicationUsers>
    {
        public PatientDbContext(DbContextOptions<PatientDbContext>option) : base(option)
        {


        }

        public DbSet<ContactUs> Contact { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }
        
            
        
    }


}
