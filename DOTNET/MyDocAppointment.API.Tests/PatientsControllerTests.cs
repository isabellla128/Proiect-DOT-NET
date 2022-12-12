using FluentAssertions;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Patients;
using MyDocAppointment.BusinessLayer.Entities;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{

    public class PatientsControllerTests : BaseIntegrationTests<PatientsController>
    {
        private const string ApiURL = "v1/api/Patients";

        public PatientsControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void When_CreatedPatient_Then_ShouldReturnPatientInTheGetRequest()
        {
            // Arrange
            CreatePatientDto createPatientDto = CreateSUT();
            // Act
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiURL, createPatientDto);
            var getPatientResult = await HttpClient.GetAsync(ApiURL);
            // Assert
            createPatientResponse.EnsureSuccessStatusCode();
            createPatientResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getPatientResult.EnsureSuccessStatusCode();
            var patients = await getPatientResult.Content.ReadFromJsonAsync<List<PatientDto>>();
            patients.Should().HaveCount(1);
            patients.Should().NotBeNull();
        }
        /*
        [Fact]
        public async void When_RegisterAppointmentsToPatient_Then_ShouldReturnAppointmentsInTheGetRequest()
        {
            // Arrange
            CreateDoctorDto createDoctorDto = CreateDoctorSUT();
            var createDoctorResponse = await HttpClient.PostAsJsonAsync("v1/api/Doctors", createDoctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();

            CreatePatientDto createPatientDto = CreateSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiURL, createPatientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            var appointments = new List<AppointmentsDtoFromDoctor>
            {
                new AppointmentsDtoFromDoctor
                {
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddHours(1),
                    PatientId = patient.Id
                },
                new AppointmentsDtoFromDoctor
                {
                    StartTime = DateTime.Now.AddDays(2),
                    EndTime = DateTime.Now.AddDays(2).AddHours(1),
                    PatientId = patient.Id
                }
            };

            // Act
            var resultResponse = await HttpClient.PostAsJsonAsync
                ($"{"v1/api/Doctors"}/{doctor.Id}/appointments", appointments);
            var getPatientrResult = await HttpClient.GetAsync($"{ApiURL}/{patient.Id}/appointments", appointments);
            // Assert
            getPatientrResult.EnsureSuccessStatusCode();
            getPatientrResult.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }*/

        [Fact]
        public async void When_DeletedPatient_Then_ShouldReturnNoPatientInTheGetRequest()
        {
            // Arrange
            CreatePatientDto createPatientDto = CreateSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiURL, createPatientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync
                ($"{ApiURL}/{patient.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        private static CreatePatientDto CreateSUT()
        {
            return new CreatePatientDto
            {
                FirstName = "Eu",
                LastName = "Tot eu",
                Email = "eu@datoteu.eu",
                Phone = "0712312312",
            };
        }
        private static CreateDoctorDto CreateDoctorSUT()
        {
            return new CreateDoctorDto
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Specialization = "Dermatology",
                Email = "doctor@gmail.com",
                Phone = "1234567890",
                Title = "doctor  docent",
                Profession = "--",
                Location = "Bosnia",
                Grade = 9,
                Reviews = 10
            };
        }
    }
}
