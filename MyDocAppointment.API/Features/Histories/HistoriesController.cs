using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MyDocAppointment.API.Features.Histories
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IRepository<History> historyRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Medication> medicationRepository;

        public HistoriesController(IRepository<History> historyRepository, IRepository<Patient> patientRepository, IRepository<Medication> medicationRepository)
        {
            this.historyRepository = historyRepository;
            this.patientRepository = patientRepository;
            this.medicationRepository = medicationRepository;
        }

        [HttpGet]
        public IActionResult GetAllHistories()
        {
            var histories = historyRepository.GetAll().Select
            (
                h => new HistoryDto
                {
                    Id = h.Id,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate
                }
             );
            return Ok(histories);
        }

        [HttpGet("{historyId:Guid}/medications")]
        public IActionResult GetMedicationsFromHistory(Guid historyId)
        {
            var history = historyRepository.GetById(historyId);
            if (history == null)
            {
                return NotFound("History with given id not found");
            }
            return Ok(history.Medications);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CreateHistoryDto historyDto)
        {
            var history = new History(historyDto.StartDate, historyDto.EndDate);
            historyRepository.Add(history);
            historyRepository.SaveChanges();
            return Created(nameof(GetAllHistories), history);
        }

        [HttpDelete("{historyId:Guid}")]
        public IActionResult DeleteHistory(Guid historyId)
        {
            try
            {
                historyRepository.Delete(historyId);
            }
            catch(ArgumentException e)
            {
                return NotFound(e.Message);
            }
            historyRepository.SaveChanges();

            return NoContent();
        }
    }
}
