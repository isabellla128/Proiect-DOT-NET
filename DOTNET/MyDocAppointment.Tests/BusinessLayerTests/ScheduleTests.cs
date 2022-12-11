using FluentAssertions;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.Tests.BusinessLayerTests
{
    public class ScheduleTests
    {
        [Fact]
        public void Given_AddEvents_IsCalled_When_Events_isEmpty_Then_Should_Return_Failure()
        {
            //arrange
            var startTime = DateTime.Now.AddDays(1);
            var endTime = DateTime.Now.AddDays(2);

            var schedule = new Schedule(startTime, endTime);
            var events = new List<Event>();
            
            //act
            var result = schedule.AddEvents(events);

            //assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("You must add at least an event");
        }

        [Fact]
        public void Given_AddMedications_IsCalled_When_MedicationsDosage_isNotEmpty_Then_Should_Return_Success()
        {
            //arrange
            var startTime = DateTime.Now.AddDays(1);
            var endTime = DateTime.Now.AddDays(2);

            var schedule = new Schedule(startTime, endTime);
            var events = new List<Event>()
            {
                new Event("event", startTime, endTime)
            };

            //act
            var result = schedule.AddEvents(events);

            //assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
