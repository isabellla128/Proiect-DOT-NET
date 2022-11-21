using FluentAssertions;
using MyDocAppointment.API.Features.Patients;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{

    public class PatientsControllerTests : BaseIntegrationTests<PatientsController>
    {
        private const string ApiURL = "v1/api/Patients";

        [Fact]
        public async void When_CreatedPatient_Then_ShouldReturnPatientInTheGetRequest()
        {
            PatientDto patientDto = createSUT();
            // Act
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiURL, patientDto);
            var getPatientResponse = await HttpClient.GetAsync(ApiURL);
            // Assert
            createPatientResponse.EnsureSuccessStatusCode();
            createPatientResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getPatientResponse.EnsureSuccessStatusCode();
            var patients = await getPatientResponse.Content.ReadFromJsonAsync<List<PatientDto>>();
            patients.Should().HaveCount(1);
            patients.Should().NotBeNull();
        }

        private static PatientDto createSUT()
        {
            // Arrange
            return new PatientDto
            {
                FirstName = "Eu",
                LastName = "Tot eu",
                Email = "eu@datoteu.eu",
                Phone = "0712312312",
            };
        }
    }
}
