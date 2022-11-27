﻿using FluentAssertions;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Patients;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class AppointmentsControllerTests : BaseIntegrationTests<DoctorsController>
    {
        private const string ApiURL = "v1/api/Appointments";

        [Fact]
        public async void When_CreatedAppointment_Then_ShouldReturnAppointmentInTheGetRequest()
        {

            // Arrange
            CreatePatientDto patientDto = CreatePatientSUT();
            CreateDoctorDto doctorDto = CreateDoctorSUT();

            var createDoctorResponse = await HttpClient.PostAsJsonAsync("v1/api/Doctors", doctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();
            
            var createPatientResponse = await HttpClient.PostAsJsonAsync("v1/api/Patients", patientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();
            
            CreateAppointmentDto appointmentDto = CreateSUT(patient.Id, doctor.Id);

            // Act
            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentDto);
            var getAppointmentResult = await HttpClient.GetAsync(ApiURL);

            // Assert
            createAppointmentResponse.EnsureSuccessStatusCode();
            createAppointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getAppointmentResult.EnsureSuccessStatusCode();
            var appointments = await getAppointmentResult.Content.ReadFromJsonAsync<List<AppointmentDto>>();
            appointments.Should().HaveCount(1);
            appointments.Should().NotBeNull();
        }

        [Fact] 
        public async void When_DeletedAppointment_Then_ShouldReturnNoAppointmentInTheGetRequest()
        {
            // Arrange
            CreatePatientDto patientDto = CreatePatientSUT();
            CreateDoctorDto doctorDto = CreateDoctorSUT();

            var createDoctorResponse = await HttpClient.PostAsJsonAsync("v1/api/Doctors", doctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();
            
            var createPatientResponse = await HttpClient.PostAsJsonAsync("v1/api/Patients", patientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            CreateAppointmentDto appointmentDto = CreateSUT(patient.Id, doctor.Id);
            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, appointmentDto);
            var appointment = await createAppointmentResponse.Content.ReadFromJsonAsync<AppointmentDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync
                ($"{ApiURL}/{appointment.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        
        private static CreatePatientDto CreatePatientSUT()
        {
            return new CreatePatientDto
            {
                FirstName = "Eu",
                LastName = "Tot eu",
                Email = "eu@datoteu.eu",
                Phone = "0712312312"
            };
        }
        private static CreateDoctorDto CreateDoctorSUT()
        {
            return new CreateDoctorDto
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Specialization = "Dermatology",
                Email = "doctor@gmail.com",
                Phone = "1234567890"
            };
        }
        private static CreateAppointmentDto CreateSUT(Guid patientId, Guid doctorId)
        {
            return new CreateAppointmentDto
            {
                DoctorId= doctorId,
                PatientId= patientId,
                StartTime = new DateTime(2023, 11, 27, 00, 29, 00),
                EndTime = new DateTime(2024, 11, 27, 00, 29, 00)
            };
        }
    }
}
