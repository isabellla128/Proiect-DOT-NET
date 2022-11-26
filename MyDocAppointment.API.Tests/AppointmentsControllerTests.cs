using FluentAssertions;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Hospitals;
using MyDocAppointment.API.Features.Patients;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class AppointmentsControllerTests : BaseIntegrationTests<DoctorsController>
    {
        private const string ApiURL = "v1/api/Appointments";

        [Fact]  //BAD REQUEST!
        public async void When_CreatedAppointment_Then_ShouldReturnAppointmentInTheGetRequest()
        {

            // Arrange
            PatientDto patientDto = CreatePatientSUT();
            DoctorDto doctorDto = CreateDoctorSUT();

            var createDoctorResponse = await HttpClient.PostAsJsonAsync("v1/api/Doctors", doctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();
            var createPatientResponse = await HttpClient.PostAsJsonAsync("v1/api/Patients", patientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            AppointmentDto appointmentDto = new AppointmentDto
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            };

            // Act
            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentDto);
            var getAppointmentResult = await HttpClient.GetAsync(ApiURL);

            // Assert
            createAppointmentResponse.EnsureSuccessStatusCode();
            createAppointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getAppointmentResult.EnsureSuccessStatusCode();
            var appointments = await getAppointmentResult.Content.ReadFromJsonAsync<List<AppointmentDto>>();
            appointments.Should().HaveCount(1);
            appointments.Should().NotBeNull();
        }

        [Fact] 
        public async void When_DeletedAppointment_Then_ShouldReturnNoAppointmentInTheGetRequest()
        {
            // Arrange
            AppointmentDto appointmentDto = CreateSUT();
            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentDto);
            var appointment = await createAppointmentResponse.Content.ReadFromJsonAsync<AppointmentDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync
                ($"{ApiURL}/{appointment.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        
        private static PatientDto CreatePatientSUT()
        {
            return new PatientDto
            {
                FirstName = "Eu",
                LastName = "Tot eu",
                Email = "eu@datoteu.eu",
                Phone = "0712312312",
            };
        }
        private static DoctorDto CreateDoctorSUT()
        {
            return new DoctorDto
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Specialization = "Dermatology",
                Email = "doctor@gmail.com",
                Phone = "1234567890"
            };
        }
        private static AppointmentDto CreateSUT()
        {
            return new AppointmentDto
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
            };
        }
    }
}
