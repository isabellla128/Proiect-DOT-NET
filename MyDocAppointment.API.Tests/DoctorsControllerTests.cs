using FluentAssertions;
using MyDocAppointment.API.Features.Doctors;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class DoctorsControllerTests : BaseIntegrationTests<DoctorsController>
    {
        private const string ApiURL = "v1/api/Doctors";

        [Fact]
        public async void When_CreatedDoctor_Then_ShouldReturnDoctorInTheGetRequest()
        {
            DoctorDto doctorDto = createSUT();
            // Act
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiURL, doctorDto);
            var getDoctorResponse = await HttpClient.GetAsync(ApiURL);
            // Assert
            createDoctorResponse.EnsureSuccessStatusCode();
            createDoctorResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getDoctorResponse.EnsureSuccessStatusCode();
            var doctors = await getDoctorResponse.Content.ReadFromJsonAsync<List<DoctorDto>>();
            doctors.Should().HaveCount(1);
            doctors.Should().NotBeNull();
        }

        private static DoctorDto createSUT()
        {
            // Arrange
            return new DoctorDto
            {
                FirstName = "Doctor",
                LastName = "Doctorescu",
                Specialization = "Diagnostic radiology",
                Email = "diagn@st.ic",
                Phone = "0712312312",
            };
        }
    }
}
