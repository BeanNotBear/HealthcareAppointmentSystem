using AutoMapper;
using HealthcareAppointment.Business.Exceptions;
using HealthcareAppointment.Data.Dtos;
using HealthcareAppointment.Data.Dtos.Appointment;
using HealthcareAppointment.Data.Repositories.AppointmentRepository;
using HealthcareAppointment.Data.Specifications;
using HealthcareAppointment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Business.Services.AppointmentService
{
	public class AppointmentService : IAppointmentService
	{
		private readonly IAppointmentRepository appointmentRepository;
		private readonly IMapper mapper;

		public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
		{
			this.appointmentRepository = appointmentRepository;
			this.mapper = mapper;
		}

		public async Task<AppointmentDto> Cancel(Guid id)
		{
			var appointmentDomain = await appointmentRepository.Cancel(id);
			if (appointmentDomain == null)
			{
				throw new NotFoundException($"Can not found appointment with id: {id}");
			}
			var appointmentDto = mapper.Map<AppointmentDto>(appointmentDomain);
			return appointmentDto;
		}

		public async Task<AppointmentDto> Create(AddAppointmentRequestDto addAppointmentRequestDto)
		{
			var appointmentDomain = mapper.Map<Appointment>(addAppointmentRequestDto);
			var createdAppointment = await appointmentRepository.Add(appointmentDomain);
			var appointmentDto = mapper.Map<AppointmentDto>(createdAppointment);
			return appointmentDto;
		}

		public async Task<bool> Delete(Guid id)
		{
			var isDeleted = await appointmentRepository.Delete(id);
			if (!isDeleted)
			{
				throw new NotFoundException($"Can not found appointment with id: {id}");
			}
			return isDeleted;
		}

		public async Task<AppointmentDto> GetById(Guid id)
		{
			var appointmentDomain = await appointmentRepository.GetById(x => x.Id == id);
			if (appointmentDomain == null)
			{
				throw new NotFoundException($"Can not found appointment with id: {id}");
			}
			var appointmentDto = mapper.Map<AppointmentDto>(appointmentDomain);
			return appointmentDto;
		}

		public async Task<PaginationList<AppointmentDto>> GetItems(AppointmentParameter appointmentParameter)
		{
			var spec = new BaseSpecification<Appointment>(x =>
				(appointmentParameter.FromDate == DateTime.MinValue || x.Date >= appointmentParameter.FromDate) &&
				(appointmentParameter.ToDate == DateTime.MaxValue || x.Date <= appointmentParameter.ToDate) &&
				(!appointmentParameter.Statuses.Any() || appointmentParameter.Statuses.Contains(x.Status))
			);
			if (!appointmentParameter.IsDescending)
			{
				spec.AddOrderBy(x => x.Date);
			}
			else
			{
				spec.AddDescending(x => x.Date);
			}
			var skip = (appointmentParameter.PageNumber - 1) * appointmentParameter.PageSize;
			var take = appointmentParameter.PageSize;
			spec.EnablePaging(take, skip);
			var appointmentDomains = await appointmentRepository.GetItems(spec);
			var appointmentDtos = mapper.Map<PaginationList<AppointmentDto>>(appointmentDomains);
			return appointmentDtos;
		}

		public async Task<PaginationList<AppointmentDto>> GetItemsByDoctorId(Guid doctorId, AppointmentParameter appointmentParameter)
		{
			var spec = new BaseSpecification<Appointment>(x =>
				x.DoctorId == doctorId &&
				(appointmentParameter.FromDate == DateTime.MinValue || x.Date >= appointmentParameter.FromDate) &&
				(appointmentParameter.ToDate == DateTime.MaxValue || x.Date <= appointmentParameter.ToDate) &&
				(!appointmentParameter.Statuses.Any() || appointmentParameter.Statuses.Contains(x.Status))
			);
			if (!appointmentParameter.IsDescending)
			{
				spec.AddOrderBy(x => x.Date);
			}
			else
			{
				spec.AddDescending(x => x.Date);
			}
			var skip = (appointmentParameter.PageNumber - 1) * appointmentParameter.PageSize;
			var take = appointmentParameter.PageSize;
			spec.EnablePaging(take, skip);
			var appointmentDomains = await appointmentRepository.GetItems(spec);
			var appointmentDtos = mapper.Map<PaginationList<AppointmentDto>>(appointmentDomains);
			return appointmentDtos;
		}

		public async Task<AppointmentDto> Update(Guid id, UpdateAppointmentRequestDto updateAppointmentRequestDto)
		{
			var appointmentDomain = mapper.Map<Appointment>(updateAppointmentRequestDto);
			appointmentDomain.Id = id;
			var updatedAppointment = await appointmentRepository.Update(x => x.Id == id, appointmentDomain);
			if (updatedAppointment == null)
			{
				throw new NotFoundException($"Can not found appointment with id: {id}");
			}
			var appointmentDto = mapper.Map<AppointmentDto>(updatedAppointment);
			return appointmentDto;
		}
	}
}
