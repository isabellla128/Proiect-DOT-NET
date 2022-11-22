using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Medications
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IRepository<Medication> medicationRepository;

        public MedicationsController(IRepository<Medication> medicationRepository)
        {
            this.medicationRepository = medicationRepository;
        }
      
        [HttpGet]
        public IActionResult GetAllMedications()
        {
            var medications = medicationRepository.GetAll().Select
            (
                m => new MedicationDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Stock = m.Stock
                }
             );
            return Ok(medications);
        }

        [HttpGet("{medicationId:Guid}")]
        public IActionResult GetMedicationById(Guid medicationId)
        {
            var medication = medicationRepository.GetById(medicationId);
            var m = new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Stock = medication.Stock
            };
            return Ok(m);
        }

         [HttpGet("{medicationId:Guid}/histories")]
         public IActionResult GetHistoryById(Guid medicationId)
         {
            var medication = medicationRepository.GetById(medicationId);
            if (medication == null)
            {
                return NotFound("Medication with given id not found");
            }
            return Ok(medication.Histories);
         }

        [HttpPost]
        public IActionResult Create([FromBody] CreateMedicationDto medicationDto)
        {
            var medication = new Medication(medicationDto.Name, medicationDto.Stock);
            medicationRepository.Add(medication);
            medicationRepository.SaveChanges();
            return Created(nameof(GetAllMedications), medication);
        }


        [HttpDelete("{medicationId:Guid}")]
        public IActionResult DeleteMedication(Guid medicationId)
        {
            try
            {
                medicationRepository.Delete(medicationId);
            }
            catch(ArgumentException e)
            {
                return NotFound(e.Message);
            }
            medicationRepository.SaveChanges();

            return NoContent();
        }
    }
}
