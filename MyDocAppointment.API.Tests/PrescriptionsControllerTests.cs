using FluentAssertions;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Medications;
using MyDocAppointment.API.Features.Patients;
using MyDocAppointment.API.Features.Prescriptions;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.API.Tests
{
    public class PrescriptionsControllerTests : BaseIntegrationTests<PrescriptionsController>
    {
        private const string ApiURL = "v1/api/Prescriptions";
        private const string ApiDoctorsURL = "v1/api/Doctors";
        private const string ApiPatientsURL = "v1/api/Patients";

        [Fact]
        public async void When_CreatedPrescription_Then_ShouldReturnPrescriptionInTheGetRequest()
        {
            // Arrange
            var createDoctorDto = CreateDoctorSUT();
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiDoctorsURL, createDoctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();


            var createPatientDto = CreatePatientSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiPatientsURL, createPatientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            CreatePrescriptionDto prescriptionDto = CreateSUT(doctor.Id, patient.Id);

            // Act
            var createPrescriptionResponse = await HttpClient.PostAsJsonAsync(ApiURL, prescriptionDto);
            var getPrescriptionResult = await HttpClient.GetAsync(ApiURL);

            // Assert
            createPrescriptionResponse.EnsureSuccessStatusCode();
            createPrescriptionResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getPrescriptionResult.EnsureSuccessStatusCode();
            var prescriptions = await getPrescriptionResult.Content.ReadFromJsonAsync<List<PrescriptionDto>>();
            prescriptions.Should().HaveCount(1);
            prescriptions.Should().NotBeNull();
        }
        [Fact]
        public async void When_RegisterMedicationsToPrescription_Then_ShouldReturnMediactionsInTheGetRequest()
        {
            // Arrange
            var createDoctorDto = CreateDoctorSUT();
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiDoctorsURL, createDoctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();


            var createPatientDto = CreatePatientSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiPatientsURL, createPatientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            CreatePrescriptionDto prescriptionDto = CreateSUT(doctor.Id, patient.Id);
            var createPrescriptionResponse = await HttpClient.PostAsJsonAsync(ApiURL, prescriptionDto);

            var medications = new List<CreateMedicationDto>
            {
                new CreateMedicationDto
                {
                    Name = "Paracetamol",
                    Stock = 10
                },
                new CreateMedicationDto
                {
                    Name = "Nurofen",
                    Stock = 15
                }
            };
            var prescription = await createPrescriptionResponse.Content.ReadFromJsonAsync<PrescriptionDto>();

            // Act
            var resultResponse = await HttpClient.PostAsJsonAsync
                ($"{ApiURL}/{prescription.Id}/medications", medications);

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        [Fact]
        public async void When_DeletedPrescription_Then_ShouldReturnNoPrescriptionInTheGetRequest()
        {
            // Arrange
            var createDoctorDto = CreateDoctorSUT();
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiDoctorsURL, createDoctorDto);
            var doctor = await createDoctorResponse.Content.ReadFromJsonAsync<DoctorDto>();


            var createPatientDto = CreatePatientSUT();
            var createPatientResponse = await HttpClient.PostAsJsonAsync(ApiPatientsURL, createPatientDto);
            var patient = await createPatientResponse.Content.ReadFromJsonAsync<PatientDto>();

            CreatePrescriptionDto prescriptionDto = CreateSUT(doctor.Id, patient.Id);
            var createPrescriptionResponse = await HttpClient.PostAsJsonAsync(ApiURL, prescriptionDto);

            var prescription = await createPrescriptionResponse.Content.ReadFromJsonAsync<PrescriptionDto>();

            // Act
            var resultResponse = await HttpClient.DeleteAsync
                ($"{ApiURL}/{prescription.Id}");

            // Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }


        private static CreatePrescriptionDto CreateSUT(Guid doctorId, Guid pacientId)
        {
            return new CreatePrescriptionDto
            {
                DoctorId = doctorId,
                PacientId = pacientId
            };
        }

        private static CreateDoctorDto CreateDoctorSUT()
        {
            return new CreateDoctorDto
            {
                FirstName = "Doctor",
                LastName = "Doctorescu",
                Specialization = "Diagnostic radiology",
                Email = "diagn@st.ic",
                Phone = "0712312312",
            };
        }

        private static CreatePatientDto CreatePatientSUT()
        {
            return new CreatePatientDto
            {
                FirstName = "Eu",
                LastName = "Tot eu",
                Email = "eu@datoteu.eu",
                Phone = "0712312312",
            };
        }
    }
}
