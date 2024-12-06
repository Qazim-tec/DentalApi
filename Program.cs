
using DentalDataBAse.Data;
using DentalDataBAse.IService;
using DentalDataBAse.Model;
using DentalDataBAse.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace DentalDataBAse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => 
            {
                opt.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Please follow follow this format. Bearer ",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey

                });

                opt.OperationFilter<SecurityRequirementsOperationFilter>();

            }
            
            );

            //string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<PatientDbContext>(option =>
            //    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<PatientDbContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddScoped<IDentalService, Dental>();
            builder.Services.AddScoped<IAppointment, AppointmentService>();
            builder.Services.AddScoped<IhomeService, HomeServService>();
            builder.Services.AddScoped<IAuths, AuthService>();

            builder.Services.AddIdentity<ApplicationUsers, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<PatientDbContext>()
                .AddSignInManager()
                .AddRoles<IdentityRole>();


            //JWT Settings
            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))

                };
            });





            // Configure CORS to allow any origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

       


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // Enable the CORS policy
            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
