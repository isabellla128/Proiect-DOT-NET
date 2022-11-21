using FluentAssertions;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Tests
{
    public class HospitalTests
    {
        [Fact]
        public void Given_AddDoctors_IsCalled_When_Doctors_isEmpty_Then_Should_Return_Failure()
        {
            //arrange
            var hospital = new Hospital("Hospital", "addr", "07324234234");

            //act
            var result = hospital.AddDoctors(new List<Doctor>());

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("you must add at least a doctor");
        }

        [Fact]
        public void Given_AddDoctors_IsCalled_When_ListIsNotEmpty_ThenShould_Return_Succcess()
        {
            //arrange
            var hospital = new Hospital("Hospital", "addr", "07324234234");
            //act
            var doctors = new List<Doctor>()
            {
                new Doctor("John", "Murray", "chirurg", "efsd.sdf@com", "07777777") 
            };
            var result = hospital.AddDoctors(doctors);
            //assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
