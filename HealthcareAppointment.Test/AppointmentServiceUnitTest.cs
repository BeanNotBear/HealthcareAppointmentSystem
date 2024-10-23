using AutoMapper;
using HealthcareAppointment.Business.Exceptions;
using HealthcareAppointment.Business.Services.AppointmentService;
using HealthcareAppointment.Data.Dtos.Appointment;
using HealthcareAppointment.Data.Repositories.AppointmentRepository;
using HealthcareAppointment.Models.Entities;
using HealthcareAppointment.Models.Enum;
using Moq;
using Xunit;
using System.Linq.Expressions;
using Assert = Xunit.Assert;

// Step by step to run test case:
//  Step 1: Open terminal 
//  Step 2: Change directory to HealthcareAppointment.UnitTest
//	Step 3: run command: dotnet test

namespace HealthcareAppointment.UnitTest
{
	public class AppointmentServiceUnitTest
	{
		private readonly Mock<IAppointmentRepository> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly IAppointmentService _appointmentService;

		public AppointmentServiceUnitTest()
		{
			_mockRepository = new Mock<IAppointmentRepository>();
			_mockMapper = new Mock<IMapper>();
			_appointmentService = new AppointmentService(_mockRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Create_WithValidAppointment_ReturnsAppointmentDto()
		{
			// Arrange
			var appointmentRequest = new AddAppointmentRequestDto
			{
				PatientId = Guid.NewGuid(),
				DoctorId = Guid.NewGuid(),
				Date = DateTime.Now.AddDays(1),
				Status = Status.Scheduled
			};

			var appointmentDomain = new Appointment
			{
				Id = Guid.NewGuid(),
				PatientId = appointmentRequest.PatientId,
				DoctorId = appointmentRequest.DoctorId,
				Date = appointmentRequest.Date,
				Status = appointmentRequest.Status
			};

			var expectedDto = new AppointmentDto
			{
				Id = appointmentDomain.Id,
				PatientId = appointmentDomain.PatientId,
				DoctorId = appointmentDomain.DoctorId,
				Date = appointmentDomain.Date,
				Status = appointmentDomain.Status.ToString()
			};

			_mockMapper.Setup(m => m.Map<Appointment>(appointmentRequest))
				.Returns(appointmentDomain);
			_mockMapper.Setup(m => m.Map<AppointmentDto>(appointmentDomain))
				.Returns(expectedDto);
			_mockRepository.Setup(r => r.Add(It.IsAny<Appointment>()))
				.ReturnsAsync(appointmentDomain);

			// Act
			var result = await _appointmentService.Create(appointmentRequest);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedDto.Id, result.Id);
			Assert.Equal(expectedDto.PatientId, result.PatientId);
			Assert.Equal(expectedDto.DoctorId, result.DoctorId);
			Assert.Equal(expectedDto.Date, result.Date);
			Assert.Equal(expectedDto.Status, result.Status);
		}

		[Fact]
		public async Task GetById_WithExistingId_ReturnsAppointmentDto()
		{
			// Arrange
			var appointmentId = Guid.NewGuid();
			var appointmentDomain = new Appointment
			{
				Id = appointmentId,
				PatientId = Guid.NewGuid(),
				DoctorId = Guid.NewGuid(),
				Date = DateTime.Now.AddDays(1),
				Status = Status.Scheduled
			};

			var expectedDto = new AppointmentDto
			{
				Id = appointmentDomain.Id,
				PatientId = appointmentDomain.PatientId,
				DoctorId = appointmentDomain.DoctorId,
				Date = appointmentDomain.Date,
				Status = appointmentDomain.Status.ToString()
			};

			_mockRepository.Setup(r => r.GetById(It.IsAny<Expression<Func<Appointment, bool>>>()))
				.ReturnsAsync(appointmentDomain);
			_mockMapper.Setup(m => m.Map<AppointmentDto>(appointmentDomain))
				.Returns(expectedDto);

			// Act
			var result = await _appointmentService.GetById(appointmentId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedDto.Id, result.Id);
			Assert.Equal(expectedDto.PatientId, result.PatientId);
			Assert.Equal(expectedDto.DoctorId, result.DoctorId);
			Assert.Equal(expectedDto.Date, result.Date);
			Assert.Equal(expectedDto.Status, result.Status);
		}

		[Fact]
		public async Task GetById_WithNonExistingId_ThrowsNotFoundException()
		{
			// Arrange
			var appointmentId = Guid.NewGuid();
			_mockRepository.Setup(r => r.GetById(It.IsAny<Expression<Func<Appointment, bool>>>()))
				.ReturnsAsync((Appointment)null);

			// Act & Assert
			await Assert.ThrowsAsync<NotFoundException>(() =>
				_appointmentService.GetById(appointmentId));
		}

		[Fact]
		public async Task GetById_CallsRepositoryWithCorrectExpression()
		{
			// Arrange
			var appointmentId = Guid.NewGuid();

			_mockRepository.Setup(r => r.GetById(It.IsAny<Expression<Func<Appointment, bool>>>()))
				.ReturnsAsync(new Appointment());

			// Act
			await _appointmentService.GetById(appointmentId);

			// Assert
			_mockRepository.Verify(r => r.GetById(It.Is<Expression<Func<Appointment, bool>>>(
				expr => expr.ToString().Contains("Id") && expr.ToString().Contains("==")
			)), Times.Once);
		}
	}
}