using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;

namespace MyDocAppointment.API.Features.Medications
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationRepositrory medicationRepository;

        public MedicationsController(IMedicationRepositrory medicationRepository)
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
                    Stock = m.Stock,
                    Unit = m.Unit,
                    Capacity = m.Capacity,
                    Price = m.Price,
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
                Stock = medication.Stock,
                Unit = medication.Unit,
                Capacity = medication.Capacity,
                Price = medication.Price,   
            };
            return Ok(m);
        }

         //[HttpGet("{medicationId:Guid}/histories")]
         //public IActionResult GetHistoryById(Guid medicationId)
         //{
         //   var medication = medicationRepository.GetById(medicationId);
         //   if (medication == null)
         //   {
         //       return NotFound("Medication with given id not found");
         //   }
         //   return Ok(medication.Histories);
         //}

        [HttpPost]
        public IActionResult Create([FromBody] CreateMedicationDto medicationDto)
        {
            var medication = new Medication(medicationDto.Name, medicationDto.Stock, medicationDto.Unit, medicationDto.Capacity, medicationDto.Price);
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
