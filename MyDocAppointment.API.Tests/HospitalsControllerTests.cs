using FluentAssertions;
using MyDocAppointment.API.Features.Hospitals;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class HospitalsControllerTests : BaseIntegrationTests<HospitalsController>
    {
        private const string ApiURL = "v1/api/Hospitals";

        [Fact]
        public async void When_CreatedHospital_Then_ShouldReturnHospitalInTheGetRequest()
        {
            HospitalDto patientDto = createSUT();
            // Act
            var createHospitalResponse = await HttpClient.PostAsJsonAsync(ApiURL, patientDto);
            var getHospitalResponse = await HttpClient.GetAsync(ApiURL);
            // Assert
            createHospitalResponse.EnsureSuccessStatusCode();
            createHospitalResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getHospitalResponse.EnsureSuccessStatusCode();
            var patients = await getHospitalResponse.Content.ReadFromJsonAsync<List<HospitalDto>>();
            patients.Should().HaveCount(1);
            patients.Should().NotBeNull();
        }

        private static HospitalDto createSUT()
        {
            // Arrange
            return new HospitalDto
            {
                Name = "Regina Maria",
                Address = "Iasi",
                Phone = "0733333333"
            };
        }
    }
}
