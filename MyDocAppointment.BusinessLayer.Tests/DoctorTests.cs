using FluentAssertions;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Tests
{
    public class DoctorTests
    {
        [Fact]
        public void Given_FullName_IsCalled_When_FirstName_And_LastName_Setted()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na","Dr.", "Medic specialist", "Iasi", 9.10, 117);
            var expected = "Jackie, Chan";
            
            //act
            var actual = doctor.FullName;
            
            //assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Given_AddHospitalToDoctor_IsCalled_When_Hospital_isNull_Then_Should_Return_Failure()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);

            //act
            var result = doctor.AddHospitalToDoctor(null);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Hospital should not be null");
        }
        
        [Fact]
        public void Given_AddHospitalToDoctor_IsCalled_When_Hospital_isNotNull_Then_Should_Return_Success()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);
            var hospital = new Hospital("Hospital", "Street", "0711111111");

            //act
            var result = doctor.AddHospitalToDoctor(hospital);

            //assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Given_AddAppointment_IsCalled_When_Appointment_isNull_Then_Should_Return_Failure()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);

            //act
            var result = doctor.AddAppointment(null);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Appointment should not be null");
        }

        [Fact]
        public void Given_AddAppointment_IsCalled_When_AppointmentStartTime_isInPast_Then_Should_Return_Failure()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);
            DateTime startTime = DateTime.Now.AddDays(-5);
            DateTime endTime = DateTime.Now;
            var appointment = new Appointment(startTime, endTime);

            //act
            var result = doctor.AddAppointment(appointment);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Appointment should be in the future");
        }

        [Fact]
        public void Given_AddAppointment_IsCalled_When_AppointmentStartTime_isAfterEndTime_Then_Should_Return_Failure()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);
            DateTime startTime = DateTime.Now.AddDays(5);
            DateTime endTime = DateTime.Now;
            var appointment = new Appointment(startTime, endTime);

            //act
            var result = doctor.AddAppointment(appointment);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Start time should be before End time");
        }

        [Fact]
        public void Given_AddAppointment_IsCalled_When_Appointment_overlaps_Then_Should_Return_Failure()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);
            DateTime startTime = DateTime.Now.AddDays(1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var appointment1 = new Appointment(startTime, endTime);
            var appointment2 = new Appointment(startTime, endTime);
            var result1 = doctor.AddAppointment(appointment1);

            //act
            var result2 = doctor.AddAppointment(appointment2);

            //assert
            result2.IsFailure.Should().BeTrue();
            result2.Error.Should().Be("A new appoinments should not intersect with a fixed appointment");
        }

        [Fact]
        public void Given_AddAppointment_IsCalled_When_Appointment_isGood_Then_Should_Return_Success()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);
            DateTime startTime = DateTime.Now.AddDays(1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var appointment = new Appointment(startTime, endTime);

            //act
            var result = doctor.AddAppointment(appointment);

            //assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Given_AddPrescription_IsCalled_When_Prescription_isNull_Then_Should_Return_Failure()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);


            //act
            var result = doctor.AddPrescription(null);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Prescription should not be null");
        }
        
        [Fact]
        public void Given_AddPrescription_IsCalled_When_Prescription_isNotNull_Then_Should_Return_Success()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na", "Dr.", "Medic specialist", "Iasi", 9.10, 117);
            var prescription = new Prescription();

            //act
            var result = doctor.AddPrescription(prescription);

            //assert
            result.IsSuccess.Should().BeTrue();
        }

    }
}
