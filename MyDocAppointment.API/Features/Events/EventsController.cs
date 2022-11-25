using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event> eventRepository;

        public EventsController(IRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = eventRepository.GetAll().Select
            (
                e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                }
             );
            return Ok(events);

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEventDto eventDto)
        {
            var e = new Event(eventDto.Name, eventDto.StartDate, eventDto.EndDate);

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
