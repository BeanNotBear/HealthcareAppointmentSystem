using HealthcareAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Data.Data
{
	public class HealthcareAppointmentDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Appointment> Appointments { get; set; }

		public HealthcareAppointmentDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(x =>
			{
				x.HasKey(x => x.Id);

				x.Property(x => x.Name)
					.IsRequired(true)
					.HasMaxLength(255)
					.HasColumnType("nvarchar");

				x.Property(x => x.Email)
					.IsRequired(true)
					.HasMaxLength(255)
					.HasColumnType("varchar");

				x.Property(x => x.DateOfBirth)
					.IsRequired(true)
					.HasColumnType("date")
					.HasDefaultValue(DateTime.Now);

				x.Property(x => x.Password)
					.IsRequired(true)
					.HasColumnType("varchar")
					.HasMaxLength(255);

				x.Property(x => x.Role)
					.IsRequired(true)
					.HasColumnType("int");

				x.Property(x => x.Specialization)
					.IsRequired(true)
					.HasColumnType("nvarchar")
					.HasMaxLength(255);

				x.HasMany(x => x.PatientAppointments)
					.WithOne(x => x.Patient)
					.HasForeignKey(x => x.PatientId)
					.OnDelete(DeleteBehavior.NoAction);

				x.HasMany(x => x.DoctorAppointments)
					.WithOne(x => x.Doctor)
					.HasForeignKey(x => x.DoctorId)
					.OnDelete(DeleteBehavior.NoAction);
			});

			modelBuilder.Entity<Appointment>(x =>
			{
				x.HasKey(x => x.Id);

				x.Property(x => x.PatientId)
					.HasColumnType("uniqueidentifier")
					.IsRequired(true);

				x.Property(x => x.DoctorId)
					.HasColumnType("uniqueidentifier")
					.IsRequired(true);

				x.Property(x => x.Date)
					.IsRequired(true)
					.HasColumnType("datetime");

				x.Property(x => x.Status)
					.IsRequired(true)
					.HasColumnType("int");
			});
		}
	}
}
