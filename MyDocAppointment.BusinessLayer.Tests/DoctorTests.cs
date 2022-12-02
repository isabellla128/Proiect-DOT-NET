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

    }
}
