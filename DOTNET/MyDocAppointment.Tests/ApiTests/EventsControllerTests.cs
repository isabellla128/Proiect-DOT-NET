using FluentAssertions;
using MyDocAppointment.API.Features.Events;
using MyDocAppointment.API.Features.Schedules;
using System.Net.Http.Json;
using Xunit;

namespace MyDocAppointment.Tests.ApiTests
{
    public class EventsControllerTests : BaseIntegrationTests<EventsController>
    {
        private const string ApiURL = "v1/api/Events";

        public EventsControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }
       
        [Fact]
        public async void When_CreatedEvent_Then_ShouldReturnEventInTheGetRequest()
        {

            // Arrange
            CreateScheduleDto createScheduleDto = CreateScheduleSUT();

            var createScheduleResponse = await HttpClient.PostAsJsonAsync("v1/api/Schedules", createScheduleDto);
            var schedule = await createScheduleResponse.Content.ReadFromJsonAsync<ScheduleDto>();
            
            schedule.Should().NotBeNull();

            if (schedule != null)
            {
                CreateEventDto createEventDto = CreateEventSUT(schedule.Id);

                // Act
                var createEventResponse = await HttpClient.PostAsJsonAsync(ApiURL, createEventDto);
                var getEventResult = await HttpClient.GetAsync(ApiURL);

                // Assert
                createEventResponse.EnsureSuccessStatusCode();
                createEventResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

                getEventResult.EnsureSuccessStatusCode();
                var appointments = await getEventResult.Content.ReadFromJsonAsync<List<EventDto>>();
                appointments.Should().HaveCount(1);
                appointments.Should().NotBeNull();
            }
        }
        
        [Fact] 
        public async void When_DeletedEvent_Then_ShouldReturnNoEventInTheGetRequest()
        {
            // Arrange
            CreateScheduleDto createScheduleDto = CreateScheduleSUT();

            var createScheduleResponse = await HttpClient.PostAsJsonAsync("v1/api/Schedules", createScheduleDto);
            var schedule = await createScheduleResponse.Content.ReadFromJsonAsync<ScheduleDto>();

            schedule.Should().NotBeNull();

            if (schedule != null)
            {
                CreateEventDto createEventDto = CreateEventSUT(schedule.Id);
                var createEventResponse = await HttpClient.PostAsJsonAsync(ApiURL, createEventDto);
                var e = await createEventResponse.Content.ReadFromJsonAsync<EventDto>();

                e.Should().NotBeNull();

                if (e != null)
                {   
                    // Act
                    var resultResponse = await HttpClient.DeleteAsync
                        ($"{ApiURL}/{e.Id}");

                    // Assert
                    resultResponse.EnsureSuccessStatusCode();
                    resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
                }
            }

        }
        
        private static CreateEventDto CreateEventSUT(Guid scheduleId)
        {
            return new CreateEventDto
            {
                Name = "Event",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                ScheduleId = scheduleId
            };
        }
        private static CreateScheduleDto CreateScheduleSUT()
        {
            return new CreateScheduleDto
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
            };
        }
    }
}
