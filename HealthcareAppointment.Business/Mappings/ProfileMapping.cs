using AutoMapper;
using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Dtos.Appointment;
using HealthcareAppointment.Data.Dtos.Doctor;
using HealthcareAppointment.Data.Dtos.Patient;
using HealthcareAppointment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Business.Mappings
{
	public class ProfileMapping : Profile
	{
		public ProfileMapping()
		{
			// Mapping for patient
			CreateMap<User, PatientDto>().ReverseMap();
			CreateMap<User, AddPatientRequestDto>().ReverseMap();
			CreateMap<User, UpdatePatientRequestDto>().ReverseMap();
			CreateMap<PaginationList<User>, PaginationList<PatientDto>>().ReverseMap();

			// Mapping for doctor
			CreateMap<User, DoctorDto>().ReverseMap();
			CreateMap<User, AddDoctorRequestDto>().ReverseMap();
			CreateMap<User, UpdateDoctorRequestDto>().ReverseMap();
			CreateMap<PaginationList<User>, PaginationList<DoctorDto>>().ReverseMap();

			// Mapping for appointment
			CreateMap<Appointment, AppointmentDto>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ReverseMap();
			CreateMap<Appointment, AddAppointmentRequestDto>().ReverseMap();
			CreateMap<Appointment, UpdateAppointmentRequestDto>().ReverseMap();
			CreateMap<PaginationList<Appointment>, PaginationList<AppointmentDto>>().ReverseMap();
		}
	}
}
