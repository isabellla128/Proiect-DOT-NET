using MyDocAppointment.API.Features.Schedules;
using Xunit;
using System.Net.Http.Json;
using FluentAssertions;
using MyDocAppointment.API.Features.Events;

namespace MyDocAppointment.API.Tests
{
    public class SchedulesControllerTests : BaseIntegrationTests<SchedulesController>
    {
        private const string ApiURL = "v1/api/Schedules";

        
        [Fact]
        public async void When_CreatedSchedule_Then_ShouldReturnScheduleInTheGetRequest()
        {
            //Arrange
            var getScheduleResult = await HttpClient.GetAsync(ApiURL);
            var schedules = await getScheduleResult.Content.ReadFromJsonAsync<List<ScheduleDto>>();
            foreach(ScheduleDto schedule1 in schedules){
                var resultResponse = await HttpClient.DeleteAsync($"{ApiURL}/{schedule1.Id}");
            }

            ScheduleDto scheduleDto = CreateSUT();

            //Act
            var createScheduleResponse=await HttpClient.PostAsJsonAsync(ApiURL, scheduleDto);
            getScheduleResult=await HttpClient.GetAsync(ApiURL);

            //Assert
            createScheduleResponse.EnsureSuccessStatusCode();
            createScheduleResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getScheduleResult.EnsureSuccessStatusCode();
            schedules = await getScheduleResult.Content.ReadFromJsonAsync<List<ScheduleDto>>();
            schedules.Should().HaveCount(1);
            schedules.Should().NotBeNull();
        }

        [Fact]
        public async void When_RegisterEventsToSchedule_Then_ShouldReturnEventsInTheGetRequest()
        {
            //Arrange
            var getScheduleResult = await HttpClient.GetAsync(ApiURL);
            var schedules = await getScheduleResult.Content.ReadFromJsonAsync<List<ScheduleDto>>();
            foreach (ScheduleDto schedule1 in schedules)
            {
                var resultResponse1 = await HttpClient.DeleteAsync($"{ApiURL}/{schedule1.Id}");
            }


            ScheduleDto scheduleDto = CreateSUT();
            var createScheduleResponse = await HttpClient.PostAsJsonAsync(ApiURL, scheduleDto);
            var events = new List<EventDto>
            {
                new EventDto
                {
                    Name="Event1",
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now
                },
                new EventDto
                {
                    Name="Event2",
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now
                }
            };
            
            var schedule = await createScheduleResponse.Content.ReadFromJsonAsync<ScheduleDto>();

            //Act
            var resultResponse=await HttpClient.PostAsJsonAsync(
                $"{ApiURL}/{schedule.Id}/events", events);

            //Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async void When_DeletedSchedule_Then_ShouldReturnNoScheduleInTheGetRequest()
        {
            //Arrange

            ScheduleDto scheduleDto = CreateSUT();
            var createScheduleResponse=await HttpClient.PostAsJsonAsync(ApiURL, scheduleDto);
            var schedule = await createScheduleResponse.Content.ReadFromJsonAsync<ScheduleDto>();

            //Act
            var resultResponse = await HttpClient.DeleteAsync($"{ApiURL}/{schedule.Id}");

            //Assert
            resultResponse.EnsureSuccessStatusCode();
            resultResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        private static ScheduleDto CreateSUT()
        {
            return new ScheduleDto
            {
                StartDate= DateTime.Now,
                EndDate= DateTime.Now,
            };
        }
    }
}
