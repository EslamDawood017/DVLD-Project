
using DVLD_BusinessLayer.Helpers;
using DVLD_BusinessLayer.interfaces;
using DVLD_BusinessLayer.Services;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Repositories;

namespace DVLD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPersonRepository, PersonRespository>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtToken, JwtToken>();
            builder.Services.AddScoped<IAppTypeService, AppTypeService>();
            builder.Services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepository>();
            builder.Services.AddScoped<ITestTypeRepository, TestTypeRepository>();
            builder.Services.AddScoped<ITestTypeService, TestTypeService>();
            builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddScoped<ILocalDrivingLicenseApplicationRepository, LocalDrivingLicenseAppRepository>();
            builder.Services.AddScoped<ILocalDrivingLicenseApplicationService, LocalDrivingLicenseApplicationService>();
            builder.Services.AddScoped<ILicenseClassService, LicenseClassService>();
            builder.Services.AddScoped<ILicenseClassRepository, LicenseClassRepository>();
            builder.Services.AddScoped<ITestAppointmentRepository, TestAppointmentRepository>();
            builder.Services.AddScoped<ITestAppointmentService, TestAppointmentService>();
            builder.Services.AddScoped<ITestService,TestServicecs>();
            builder.Services.AddScoped<ITestRepository, TestRepository>();
            builder.Services.AddScoped<ILicenseService, LicenseService>();
            builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IdriverRepository, DriverRepository>();
            builder.Services.AddScoped<IDetainedLicenseService, DetainedLicenseService>();
            builder.Services.AddScoped<IDetainedRepository, DetainedLicenseRepository>();
            builder.Services.AddScoped<IInternationalLicenseRepository, InternationalLicenseRepository>();
            builder.Services.AddScoped<IInternationalService, InternationalService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin() 
                              .AllowAnyMethod() 
                              .AllowAnyHeader(); 
                    });
            });
            builder.Services.AddControllers();



            var app = builder.Build();

            app.UseStaticFiles();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
