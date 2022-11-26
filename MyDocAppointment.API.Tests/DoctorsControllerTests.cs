using FluentAssertions;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Patients;
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
            // Arrange
            DoctorDto doctorDto = CreateSUT();
            // Act
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiURL, doctorDto);
            var getDoctorResult = await HttpClient.GetAsync(ApiURL);
            // Assert
            createDoctorResponse.EnsureSuccessStatusCode();
            createDoctorResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getDoctorResult.EnsureSuccessStatusCode();
            var doctors = await getDoctorResult.Content.ReadFromJsonAsync<List<DoctorDto>>();
            doctors.Should().HaveCount(1);
            doctors.Should().NotBeNull();
        }

        [Fact]  //NU MERGE NUJ DC AM FACUT-O DUPA (REGISTER DOCTORS TOH OSPITAL) SI AIA MERGE SI ASTA NU
        public async void When_RegisterAppointmentsToDoctor_Then_ShouldReturnAppointmentsInTheGetRequest()
        {
            // Arrange
            DoctorDto doctorDto = CreateSUT();
            
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiURL, doctorDto);
           
            var appointments = new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now
      
                },
                new AppointmentDto
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now
                }
            };
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();

            // Act
            var resultResponse = await HttpClient.PostAsJsonAsync
                ($"{ApiURL}/{doctor.Id}/appointments", appointments);

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async void When_DeletedDoctor_Then_ShouldReturnNoDoctorInTheGetRequest()
        {
            // Arrange
            DoctorDto doctorDto = CreateSUT();
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiURL, doctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync 
                ($"{ApiURL}/{doctor.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        private static DoctorDto CreateSUT()
        {
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
