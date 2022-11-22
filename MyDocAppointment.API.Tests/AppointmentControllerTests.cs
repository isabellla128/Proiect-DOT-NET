using FluentAssertions;
using MyDocAppointment.API.Features.Appointments;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class AppointmentControllerTests : BaseIntegrationTests<AppointmentsController>
    {
        private const string ApiURL = "v1/api/Appointments";

        [Fact]
        public async void When_CreatedAppointment_Then_ShouldReturnAppointmentInTheGetRequest()
        {
            AppointmentDto appointmentDto = createSUT();
            // Act
            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentDto);
            var getAppointmentResponse = await HttpClient.GetAsync(ApiURL);
            // Assert
            createAppointmentResponse.EnsureSuccessStatusCode();
            createAppointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getAppointmentResponse.EnsureSuccessStatusCode();
            var appointments = await getAppointmentResponse.Content.ReadFromJsonAsync<List<AppointmentDto>>();
            appointments.Should().HaveCount(1);
            appointments.Should().NotBeNull();
        }

        private static AppointmentDto createSUT()
        {
            // Arrange
            return new AppointmentDto
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),

            };
        }
    }
}
