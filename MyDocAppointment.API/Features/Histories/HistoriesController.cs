using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;

namespace MyDocAppointment.API.Features.Histories
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryRepository1 historyRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IMedicationRepositrory medicationRepository;

        public HistoriesController(IHistoryRepository1 historyRepository, IPatientRepository patientRepository, IMedicationRepositrory medicationRepository)
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
            return Ok(history.MedicationDosageHistories);
        }
        
        [HttpPost]
        public IActionResult Create(Guid patientId, [FromBody] CreateHistoryDto historyDto)
        {
            var history = new History(historyDto.StartDate, historyDto.EndDate);

            var patient = patientRepository.GetById(patientId);
            if(patient == null)
            {
                return BadRequest("Patient with given id not found");
            }
            var result = history.AddPatientToHistory(patient);
            if(result.IsFailure) 
            {
                return BadRequest(result.Error);
            }

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
