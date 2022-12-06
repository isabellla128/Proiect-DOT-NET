﻿using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;

namespace MyDocAppointment.API.Features.Events
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepositrory eventRepository;
        private readonly IScheduleRepository scheduleRepository;

        public EventsController(IEventRepositrory eventRepository, IScheduleRepository scheduleRepository)
        {
            this.eventRepository = eventRepository;
            this.scheduleRepository = scheduleRepository;
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
        public IActionResult Create(Guid scheduleId, [FromBody] CreateEventDto eventDto)
        {
            var e = new Event(eventDto.Name, eventDto.StartDate, eventDto.EndDate);

            var schedule = scheduleRepository.GetById(scheduleId);
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
