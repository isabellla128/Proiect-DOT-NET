using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Events
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event> eventRepository;
        private readonly IRepository<Schedule> scheduleRepository;
        private readonly IMapper mapper;

        public EventsController(IRepository<Event> eventRepository, IRepository<Schedule> scheduleRepository,IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = eventRepository.GetAll().Result;
            var eventsDto = mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(eventsDto);

        }

        [HttpPost]
        public IActionResult Create(Guid scheduleId, [FromBody] CreateEventDto eventDto)
        {
            var e = new Event(eventDto.Name, eventDto.StartDate, eventDto.EndDate);

            var schedule = scheduleRepository.GetById(scheduleId).Result;
            if(schedule == null)
            {
                return BadRequest("Schedule with given id not found");
            }

            e.AddScheduleToEvent(schedule);
            eventRepository.Add(e);
            eventRepository.SaveChanges();
            return Created(nameof(GetAllEvents), e);
        }

        [HttpDelete("{eventId:Guid}")]
        public IActionResult DeleteEvent(Guid eventId)
        {
            try
            {
                eventRepository.Delete(eventId);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            eventRepository.SaveChanges();

            return NoContent();
        }
    }
}
