﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Histories;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Medications
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IRepository<Medication> medicationRepository;
        private readonly IMapper mapper;

        public MedicationsController(IRepository<Medication> medicationRepository, IMapper mapper)
        {
            this.medicationRepository = medicationRepository;
            this.mapper = mapper;
        }
      
        [HttpGet]
        public IActionResult GetAllMedications()
        {
            var medications = medicationRepository.GetAll().Result;
            var medicationsDto = mapper.Map<IEnumerable<MedicationDto>>(medications);
            return Ok(medicationsDto);
        }

        [HttpGet("{medicationId:Guid}")]
        public IActionResult GetMedicationById(Guid medicationId)
        {
            var medication = medicationRepository.GetById(medicationId).Result;

            if (medication == null)
            {
                return NotFound("There is no medication with given id");
            }

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

        [HttpPut("{medicationId:Guid}")]
        public IActionResult UpdateMedication(Guid medicationId, [FromBody] Medication medication)
        {
            var medicationToChange = medicationRepository.GetById(medicationId).Result;

            if(medicationToChange==null)
            {
                return NotFound("There is no medication with the given id");
            }

            medicationToChange.UpdateMedication(medication);

            medicationRepository.SaveChanges();
            return Ok(medicationToChange);
        }
    }
}
