using FluentAssertions;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Hospitals;
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
    public class HospitalsControllerTests : BaseIntegrationTests<HospitalsController>
    {
        private const string ApiURL = "v1/api/Hospitals";

        [Fact]
        public async void When_CreatedHospital_Then_ShouldReturnHospitalInTheGetRequest()
        {
            // Arrange
            CreateHospitalDto createHospitalDto = CreateSUT();

            // Act
            var createHospitalResponse = await HttpClient.PostAsJsonAsync(ApiURL, createHospitalDto);
            var getHospitalResult = await HttpClient.GetAsync(ApiURL);

            // Assert
            createHospitalResponse.EnsureSuccessStatusCode();
            createHospitalResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getHospitalResult.EnsureSuccessStatusCode();
            var hospitals = await getHospitalResult.Content.ReadFromJsonAsync<List<HospitalDto>>();
            hospitals.Should().HaveCount(1);
            hospitals.Should().NotBeNull();
        }


        [Fact]
        public async void When_RegisterDoctorsToHospital_Then_ShouldReturnDoctorsInTheGetRequest()
        {
            // Arrange
            CreateHospitalDto createHospitalDto = CreateSUT();
            var createHospitalResponse = await HttpClient.PostAsJsonAsync(ApiURL, createHospitalDto);

            var doctors = new List<DoctorDto>
            {
                new DoctorDto
                {
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    Specialization = "Dermatology",
                    Email = "doctor@gmail.com",
                    Phone = "1234567890"
                },
                new DoctorDto
                {
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    Specialization = "All",
                    Email = "doctoooooor@gmail.com",
                    Phone = "122222222"
                }
            };
            var hospital = await createHospitalResponse.Content.ReadFromJsonAsync<HospitalDto>();

            // Act
            var resultResponse = await HttpClient.PostAsJsonAsync
                ($"{ApiURL}/{hospital.Id}/doctors", doctors);

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact] 
        public async void When_DeletedHospital_Then_ShouldReturnNoHospitalInTheGetRequest()
        {
            // Arrange
            CreateHospitalDto createHospitalDto = CreateSUT();
            var createHostpitalResponse = await HttpClient.PostAsJsonAsync(ApiURL, createHospitalDto);
            var hospital = await createHostpitalResponse.Content.ReadFromJsonAsync<DoctorDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync 
                ($"{ApiURL}/{hospital.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        private static CreateHospitalDto CreateSUT()
        {
            return new CreateHospitalDto
            {
                Name = "Regina Maria",
                Address = "Iasi",
                Phone = "0733333333"
            };
        }
    }
}
