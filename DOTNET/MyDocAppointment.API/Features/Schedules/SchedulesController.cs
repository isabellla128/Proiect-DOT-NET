using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Events;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Schedules
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IRepository<Schedule> scheduleRepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IMapper mapper;

        public SchedulesController(IRepository<Schedule> scheduleRepository, IRepository<Event> eventRepository, IMapper mapper)
        {
            this.scheduleRepository = scheduleRepository;
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllSchedules()
        {
            var schedules = scheduleRepository.GetAll().Result;
            var schedulesDto = mapper.Map<IEnumerable<ScheduleDto>>(schedules);

            return Ok(schedulesDto);
        }

        [HttpGet("{scheduleId:Guid}/events")]
        public IActionResult GetAllEventsFromSchedule(Guid scheduleId)
        {
            var events = eventRepository.Find(e => e.ScheduleId == scheduleId).Result;
            return Ok(events.Select(
                e => new EventDto
                {
                    Id=e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    EndDate=e.EndDate
                }));

        }

        [HttpPost]
        public IActionResult CreateSchedule([FromBody] CreateScheduleDto scheduleDto)
        {
            var schedule = new Schedule(scheduleDto.StartDate, scheduleDto.EndDate);
            scheduleRepository.Add(schedule);
            scheduleRepository.SaveChanges();
            return Created(nameof(GetAllSchedules), schedule);
        }

        [HttpPost("{scheduleId:Guid}/events")]
        public IActionResult RegisterNewEventsToSchedule(Guid scheduleId, [FromBody] List<CreateEventDto> eventsDtos)
        {

            var schedule = scheduleRepository.GetById(scheduleId).Result;
            if (schedule == null)
            {
                return NotFound("Schedule with given id not found");
            }

            var events = eventsDtos.Select(e => new Event(e.Name, e.StartDate, e.EndDate)).ToList();

            var result = schedule.AddEvents(events);


            events.ForEach(e =>
            {
                eventRepository.Add(e);
            });
            eventRepository.SaveChanges();

            return result.IsSuccess ? NoContent() : BadRequest();
        }

        [HttpDelete("{scheduleId:Guid}")]
        public IActionResult DeleteSchedule(Guid scheduleId)
        {
            scheduleRepository.Delete(scheduleId);
            scheduleRepository.SaveChanges();

            return NoContent();
        }
    }
}
