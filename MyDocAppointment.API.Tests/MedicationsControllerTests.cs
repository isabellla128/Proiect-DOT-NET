using FluentAssertions;
using MyDocAppointment.API.Features.Medications;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class MedicationsControllerTests : BaseIntegrationTests<MedicationsController>
    {
        private const string ApiURL = "v1/api/Medications";

        public MedicationsControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async void When_CreatedMedication_Then_ShouldReturnMedicationInTheGetRequest()
        {
            // Arrange
            CreateMedicationDto createMedicationDto = CreateSUT();
            // Act
            var createMedicationResponse = await httpClient.PostAsJsonAsync(ApiURL, createMedicationDto);
            var getMedicationResult = await httpClient.GetAsync(ApiURL);
            // Assert
            createMedicationResponse.EnsureSuccessStatusCode();
            createMedicationResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getMedicationResult.EnsureSuccessStatusCode();
            var doctors = await getMedicationResult.Content.ReadFromJsonAsync<List<MedicationDto>>();
            doctors.Should().HaveCount(1);
            doctors.Should().NotBeNull();
        }

        [Fact]
        public async void When_DeletedMedication_Then_ShouldReturnNoMedicationInTheGetRequest()
        {
            // Arrange
            CreateMedicationDto createMedicationDto = CreateSUT();
            var createMedicationResponse = await httpClient.PostAsJsonAsync(ApiURL, createMedicationDto);
            var medication = await createMedicationResponse.Content.ReadFromJsonAsync<MedicationDto>();

            // Act
            var resultResponse = await httpClient.DeleteAsync
                ($"{ApiURL}/{medication.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        private static CreateMedicationDto CreateSUT()
        {
            return new CreateMedicationDto
            {
                Name = "Paracetamol",
                Stock = 21,
                Unit = "capsule",
                Capacity= 1,
                Price= 1
            };
        }

    }

}
