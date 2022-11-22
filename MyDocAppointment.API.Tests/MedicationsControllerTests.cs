using FluentAssertions;
using MyDocAppointment.API.Features.Medications;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class MedicationsControllerTests : BaseIntegrationTests<MedicationsController>
    {
        private const string ApiURL = "v1/api/Medications";

        [Fact]
        public async void When_CreatedMedication_Then_ShouldReturnMedicationInTheGetRequest()
        {
            MedicationDto medicationDto = createSUT();
            // Act
            var createMedicationResponse = await HttpClient.PostAsJsonAsync(ApiURL, medicationDto);
            var getMedicationResponse = await HttpClient.GetAsync(ApiURL);
            // Assert
            createMedicationResponse.EnsureSuccessStatusCode();
            createMedicationResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getMedicationResponse.EnsureSuccessStatusCode();
            var medications = await getMedicationResponse.Content.ReadFromJsonAsync<List<MedicationDto>>();
            medications.Should().HaveCount(1);
            medications.Should().NotBeNull();
        }

        private static MedicationDto createSUT()
        {
            // Arrange
            return new MedicationDto
            {
                Name = "Paracetamol",
                Stock = 99
               
            };
        }
    }
}
