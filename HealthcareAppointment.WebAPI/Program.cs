using HealthcareAppointment.Business.Mappings;
using HealthcareAppointment.Business.Services.AppointmentService;
using HealthcareAppointment.Business.Services.DoctorService;
using HealthcareAppointment.Business.Services.PatientService;
using HealthcareAppointment.Data.Data;
using HealthcareAppointment.Data.Repositories.AppointmentRepository;
using HealthcareAppointment.Data.Repositories.AuthRepository;
using HealthcareAppointment.Data.Repositories.BaseRepository;
using HealthcareAppointment.Data.Repositories.DoctorRepository;
using HealthcareAppointment.Data.Repositories.PatientRepository;
using HealthcareAppointment.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure sql
builder.Services.AddDbContext<HealthcareAppointmentDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure automapper
builder.Services.AddAutoMapper(typeof(ProfileMapping));
builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

// DI for patient
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

// DI for doctor
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

// DI for appointment
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
