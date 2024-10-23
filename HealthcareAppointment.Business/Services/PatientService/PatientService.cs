using AutoMapper;
using HealthcareAppointment.Business.Exceptions;
using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Dtos.Patient;
using HealthcareAppointment.Data.Repositories.PatientRepository;
using HealthcareAppointment.Data.Specifications;
using HealthcareAppointment.Models.Entities;
using HealthcareAppointment.Models.Enum;

namespace HealthcareAppointment.Business.Services.PatientService
{
	public class PatientService : IPatientService
	{
		private readonly IPatientRepository patientRepository;
		private readonly IMapper mapper;

		public PatientService(IPatientRepository patientRepository, IMapper mapper)
		{
			this.patientRepository = patientRepository;
			this.mapper = mapper;
		}

		public async Task<PatientDto> Create(AddPatientRequestDto addPatientRequest)
		{
			var patientDomain = mapper.Map<User>(addPatientRequest);
			patientDomain.Role = Role.Patient;
			var createdPatient = await patientRepository.Add(patientDomain);
			var patientDto = mapper.Map<PatientDto>(createdPatient);
			return patientDto;
		}

		public async Task<bool> Delete(Guid id)
		{
			var isDeleted = await patientRepository.Delete(id);
			if (!isDeleted)
			{
				throw new NotFoundException($"Can not found patient with id: {id}");
			}
			return isDeleted;
		}

		public async Task<PatientDto> GetById(Guid id)
		{
			var patientDomain = await patientRepository.GetById(x => x.Role == Role.Patient && x.Id == id);
			if(patientDomain == null)
			{
				throw new NotFoundException($"Can not found patient with id: {id}");
			}
			var patientDto = mapper.Map<PatientDto>(patientDomain);
			return patientDto;
		}

		public async Task<PaginationList<PatientDto>> GetItems(PatientParameter patientParameter)
		{
			var spec = new BaseSpecification<User>(x =>
				x.Role == Role.Patient &&
				(string.IsNullOrWhiteSpace(patientParameter.Search) || x.Name.Contains(patientParameter.Search) || x.Email.Contains(patientParameter.Search))
			);

			if (!patientParameter.IsDescending)
			{
				spec.AddOrderBy(x => x.Name);
			}
			else
			{
				spec.AddDescending(x => x.Name);
			}

			var take = patientParameter.PageSize;
			var skip = (patientParameter.PageNumber - 1) * take;

			spec.EnablePaging(take, skip);

			var patientDomains = await patientRepository.GetItems(spec);
			var patientDtos = mapper.Map<PaginationList<PatientDto>>(patientDomains);
			return patientDtos;
		}

		public async Task<PatientDto> Update(Guid id, UpdatePatientRequestDto updatePatientRequest)
		{
			var patientDomain = mapper.Map<User>(updatePatientRequest);
			if(patientDomain == null)
			{
				throw new NotFoundException($"Can not found patient with id: {id}");
			}
			patientDomain.Id = id;
			var updatedPatient = await patientRepository.Update(x => x.Id == id && x.Role == Role.Patient, patientDomain);
			var patientDto = mapper.Map<PatientDto>(updatedPatient);
			return patientDto;
		}
	}
}
