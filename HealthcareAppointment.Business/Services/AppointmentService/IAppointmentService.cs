using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Dtos.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Business.Services.AppointmentService
{
	public interface IAppointmentService
	{
		public Task<PaginationList<AppointmentDto>> GetItems(AppointmentParameter appointmentParameter);
		public Task<AppointmentDto> GetById(Guid id);
		public Task<AppointmentDto> Create(AddAppointmentRequestDto addAppointmentRequestDto);
		public Task<AppointmentDto> Update(Guid id, UpdateAppointmentRequestDto updateAppointmentRequestDto);
		public Task<bool> Delete(Guid id);

		public Task<AppointmentDto> Cancel(Guid id);
		public Task<PaginationList<AppointmentDto>> GetItemsByDoctorId(Guid doctorId, AppointmentParameter appointmentParameter);
	}
}
