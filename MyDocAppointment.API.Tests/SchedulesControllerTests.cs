using MyDocAppointment.API.Features.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ScheduleDto scheduleDto = CreateSUT();

            //Act
            var createScheduleResponse=await HttpClient.PostAsJsonAsync(ApiURL, scheduleDto);
            var getScheduleResult=await HttpClient.GetAsync(ApiURL);

            //Assert
            createScheduleResponse.EnsureSuccessStatusCode();
            createScheduleResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            getScheduleResult.EnsureSuccessStatusCode();
            var schedules = await getScheduleResult.Content.ReadFromJsonAsync<List<ScheduleDto>>();
            schedules.Should().HaveCount(1);
            schedules.Should().NotBeNull();

            //merge pentru prima rulare, nu pot sa fac clear la Schedules
        }

        [Fact]
        public async void When_RegisterEventsToSchedule_Then_ShouldReturnEventsInTheGetRequest()
        {
            //Arrange
            ScheduleDto scheduleDto = CreateSUT();
            var createScheduleResponse=await HttpClient.PatchAsJsonAsync(ApiURL, scheduleDto);
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
            
            var schedule = await createScheduleResponse.Content.ReadFromJsonAsync<EventDto>();

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
