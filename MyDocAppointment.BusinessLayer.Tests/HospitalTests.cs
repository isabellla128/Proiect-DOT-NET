using MyDocAppointment.BusinessLayer.Entities;
using ShelterManagement.Business.Helpers;

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
            Assert.True(result.IsFailure);
            Assert.Equal("you must add at least a doctor", result.Error);
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
            Assert.True(result.IsSuccess);
        }
    }
}
