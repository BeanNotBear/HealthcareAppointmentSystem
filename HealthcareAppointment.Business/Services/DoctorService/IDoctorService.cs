using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Dtos.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Business.Services.DoctorService
{
	public interface IDoctorService
	{
		Task<PaginationList<DoctorDto>> GetItems(DoctorParameter parameter);
		Task<DoctorDto> GetById(Guid id);
		Task<DoctorDto> Create(AddDoctorRequestDto addDoctorRequestDto);
		public Task<DoctorDto> Update(Guid id, UpdateDoctorRequestDto updateDoctorRequestDto);
		Task<bool> Delete(Guid id);
	}
}
