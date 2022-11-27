using FluentAssertions;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Histories;
using MyDocAppointment.API.Features.Patients;
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
    public class HistoriesControllerTests : BaseIntegrationTests<HistoriesController>
    {
        private const string ApiURL = "v1/api/Histories";

        [Fact]
        public async void When_CreatedHistories_Then_ShouldReturnHistoryInTheGetRequest()
        {
            PatientDto patientDto = CreatePatientSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync("v1/api/Patients", patientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();
            createPatientResponse.EnsureSuccessStatusCode();
            createPatientResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);


            HistoryDto historyDto = CreateSUT();
             // Act
            var createHistoryResponse = await HttpClient.PostAsJsonAsync($"{ApiURL}?patientId={patient.Id}", historyDto);
            var getHistoryResult = await HttpClient.GetAsync(ApiURL);

            // Assert
            createHistoryResponse.EnsureSuccessStatusCode();
            createHistoryResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getHistoryResult.EnsureSuccessStatusCode();
            var histories = await getHistoryResult.Content.ReadFromJsonAsync<List<HistoryDto>>();
            histories.Should().HaveCount(1);
            histories.Should().NotBeNull();
        }

        [Fact]
        public async void When_DeletedHistory_Then_ShouldReturnNoHistoryInTheGetRequest()
        {
            // Arrange

            PatientDto patientDto = CreatePatientSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync("v1/api/Patients", patientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();
            createPatientResponse.EnsureSuccessStatusCode();
            createPatientResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            HistoryDto historyDto = CreateSUT();
            var createHistoryResponse = await HttpClient.PostAsJsonAsync($"{ApiURL}?patientId={patient.Id}", historyDto);
            var history = await createHistoryResponse.Content.ReadFromJsonAsync<HistoryDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync
                ($"{ApiURL}/{history.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        private static HistoryDto CreateSUT()
        {
            return new HistoryDto
            {
                StartDate = new DateTime(2023, 11, 27, 00, 29, 00),
                EndDate = new DateTime(2024, 11, 27, 00, 29, 00)
            };
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
    }
}
