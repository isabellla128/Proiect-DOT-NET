using MyDocAppointment.BusinessLayer.Entities;
using Xunit.Sdk;

namespace MyDocAppointment.BusinessLayer.Tests
{
    public class DoctorTests
    {
        [Fact]
        public void Given_FullName_IsCalled_When_FirstName_And_LastName_Setted()
        {
            //arrange
            var doctor = new Doctor("Jackie", "Chan", "all", "na", "na");
            var expected = "Jackie, Chan";
            //act
            var actual = doctor.FullName;
            //assert
            Assert.Equal(actual, expected);
        }

    }
}
