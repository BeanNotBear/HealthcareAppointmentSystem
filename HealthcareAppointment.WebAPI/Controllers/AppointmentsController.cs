using HealthcareAppointment.Business.Services.AppointmentService;
using HealthcareAppointment.Data.Attribute;
using HealthcareAppointment.Data.Dtos.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointment.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ValidateModel]
	public class AppointmentsController : ControllerBase
	{
		private readonly IAppointmentService appointmentService;

		public AppointmentsController(IAppointmentService appointmentService)
		{
			this.appointmentService = appointmentService;
		}

		[HttpGet]
		public async Task<IActionResult> GetItems([FromQuery] AppointmentParameter appointmentParameter)
		{
			var items = await appointmentService.GetItems(appointmentParameter);
			return Ok(items);
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var appointmentDto = await appointmentService.GetById(id);
			return Ok(appointmentDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddAppointmentRequestDto addAppointmentRequestDto)
		{
			var appointmentDto = await appointmentService.Create(addAppointmentRequestDto);
			return CreatedAtAction(nameof(GetById), new { id = appointmentDto.Id }, appointmentDto);
		}

		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAppointmentRequestDto updateAppointmentRequestDto)
		{
			var appointmentDto = await appointmentService.Update(id, updateAppointmentRequestDto);
			return Ok(appointmentDto);
		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var isDeleted = await appointmentService.Delete(id);
			return NoContent();
		}

		[HttpPatch]
		[Route("{id:guid}/cancel")]
		public async Task<IActionResult> Cancel([FromRoute] Guid id)
		{
			var appointmentDto = await appointmentService.Cancel(id);
			return Ok(appointmentDto);
		}

		[HttpGet]
		[Route("{doctorId:guid}/Search")]
		public async Task<IActionResult> GetItemsByDoctorId([FromRoute] Guid doctorId, [FromQuery] AppointmentParameter appointmentParameter)
		{
			var appointmentDtos = await appointmentService.GetItemsByDoctorId(doctorId, appointmentParameter);
			return Ok(appointmentDtos);
		}
	}
}
