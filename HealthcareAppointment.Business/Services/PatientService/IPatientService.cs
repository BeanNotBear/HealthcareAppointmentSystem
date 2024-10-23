using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Dtos.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Business.Services.PatientService
{
	public interface IPatientService
	{
		public Task<PaginationList<PatientDto>> GetItems(PatientParameter patientParameter);
		public Task<PatientDto> GetById(Guid id);
		public Task<PatientDto> Create(AddPatientRequestDto addPatientRequest);
		public Task<PatientDto> Update(Guid id, UpdatePatientRequestDto updatePatientRequest);
		public Task<bool> Delete(Guid id);
	}
}
