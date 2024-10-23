using HealthcareAppointment.Business.Services.PatientService;
using HealthcareAppointment.Data.Attribute;
using HealthcareAppointment.Data.Dtos.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointment.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ValidateModel]
	public class PatientsController : ControllerBase
	{
		private readonly IPatientService patientService;

		public PatientsController(IPatientService patientService)
		{
			this.patientService = patientService;
		}

		[HttpGet]
		public async Task<IActionResult> GetItems([FromQuery] PatientParameter patientParameter)
		{
			var items = await patientService.GetItems(patientParameter);
			return Ok(items);
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var patientDto = await patientService.GetById(id);
			return Ok(patientDto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddPatientRequestDto addPatientRequestDto)
		{
			var patientDto = await patientService.Create(addPatientRequestDto);
			return CreatedAtAction(nameof(GetById), new { id = patientDto.Id }, patientDto);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePatientRequestDto updatePatientRequestDto)
		{
			var patientDto = await patientService.Update(id, updatePatientRequestDto);
			return Ok(patientDto);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var isDeleted = await patientService.Delete(id);
			return NoContent();
		}
	}
}
