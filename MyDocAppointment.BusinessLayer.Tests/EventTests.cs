using FluentAssertions;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Tests
{
    public class EventTests
    {
        [Fact]
        public void Given_AddScheduleToEvent_IsCalled_When_Schedule_isNull_Then_Should_Return_Failure()
        {
            //arrange
            DateTime startTime = DateTime.Now.AddDays(1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var newEvent = new Event("NewEvent", startTime, endTime);

            //act
            var result = newEvent.AddScheduleToEvent(null);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Schedule should not be null");
        }

        [Fact]
        public void Given_AddHospitalToDoctor_IsCalled_When_Hospital_isNotNull_Then_Should_Return_Success()
        {
            //arrange
            DateTime startTime = DateTime.Now.AddDays(1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var newEvent = new Event("NewEvent", startTime, endTime);
            var schedule = new Schedule(startTime, endTime);

            //act
            var result = newEvent.AddScheduleToEvent(schedule);

            //assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Given_ValidateName_IsCalled_When_Name_isWhiteSpace_Then_Should_Return_False()
        {
            //arrange
            DateTime startTime = DateTime.Now.AddDays(1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var newEvent = new Event(" ", startTime, endTime);

            //act
            var result = newEvent.ValidateName();

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Given_IsStartDateValid_When_Start_isInFuture_Then_Should_Return_True()
        {
            DateTime startTime = DateTime.Now.AddDays(1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var newEvent = new Event("event", startTime, endTime);

            Assert.True(newEvent.IsStartDateValid());

        }
        
        [Fact]
        public void Given_IsStartDateValid_When_Start_isInPast_Then_Should_Return_False()
        {
            DateTime startTime = DateTime.Now.AddDays(-1);
            DateTime endTime = DateTime.Now.AddDays(2);
            var newEvent = new Event("event", startTime, endTime);

            Assert.False(newEvent.IsStartDateValid());

        }

    }
}
