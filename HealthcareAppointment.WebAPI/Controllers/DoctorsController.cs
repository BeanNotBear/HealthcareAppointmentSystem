using HealthcareAppointment.Business.Services.DoctorService;
using HealthcareAppointment.Data.Attribute;
using HealthcareAppointment.Data.Dtos.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointment.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ValidateModel]
	public class DoctorsController : ControllerBase
	{
		private readonly IDoctorService doctorService;

		public DoctorsController(IDoctorService doctorService)
		{
			this.doctorService = doctorService;
		}

		[HttpGet]
		public async Task<IActionResult> GetItems([FromQuery] DoctorParameter doctorParameter)
		{
			var doctorDtos = await doctorService.GetItems(doctorParameter);
			return Ok(doctorDtos);
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var doctorDto = await doctorService.GetById(id);
			return Ok(doctorDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddDoctorRequestDto addDoctorRequestDto)
		{
			var doctorDto = await doctorService.Create(addDoctorRequestDto);
			return CreatedAtAction(nameof(GetById), new { id = doctorDto.Id }, doctorDto);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDoctorRequestDto updateDoctorRequestDto)
		{
			var doctorDto = await doctorService.Update(id, updateDoctorRequestDto);
			return Ok(doctorDto);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var isDeleted = await doctorService.Delete(id);
			return NoContent();
		}
	}
}
