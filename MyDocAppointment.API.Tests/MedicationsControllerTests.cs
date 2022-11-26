using FluentAssertions;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Histories;
using MyDocAppointment.API.Features.Medications;
using MyDocAppointment.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class MedicationsControllerTests : BaseIntegrationTests<MedicationsController>
    {
        private const string ApiURL = "v1/api/Medications";
        [Fact]
        public async void When_CreatedMedication_Then_ShouldReturnMedicationInTheGetRequest()
        {
            // Arrange
            MedicationDto medicationDto = CreateSUT();
            // Act
            var createMedicationResponse = await HttpClient.PostAsJsonAsync(ApiURL, medicationDto);
            var getMedicationResult = await HttpClient.GetAsync(ApiURL);
            // Assert
            createMedicationResponse.EnsureSuccessStatusCode();
            createMedicationResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getMedicationResult.EnsureSuccessStatusCode();
            var doctors = await getMedicationResult.Content.ReadFromJsonAsync<List<MedicationDto>>();
            doctors.Should().HaveCount(1);
            doctors.Should().NotBeNull();
        }
        private static MedicationDto CreateSUT()
        {
            return new MedicationDto
            {
                Name = "Paracetamol",
                Stock = 21
            };
        }

    }

}
