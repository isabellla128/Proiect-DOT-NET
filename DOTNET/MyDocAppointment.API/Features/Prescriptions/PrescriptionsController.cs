using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Medications;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Prescriptions
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        public readonly IRepository<Prescription> prescriptionRepository;
        public readonly IRepository<Doctor> doctorRepository;
        public readonly IRepository<Patient> patientRepository;
        private readonly IRepository<MedicationDosagePrescription> medicationDosageRepository;
        private readonly IRepository<Medication> medicationRepository;
        private readonly IMapper mapper;

        public PrescriptionsController(IRepository<Prescription> prescriptionRepository, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository, IRepository<Medication> medicationRepository, IRepository<MedicationDosagePrescription> medicationDosageRepository, IMapper mapper)
        {
            this.prescriptionRepository=prescriptionRepository;
            this.doctorRepository=doctorRepository;
            this.patientRepository = patientRepository;
            this.medicationDosageRepository = medicationDosageRepository;
            this.medicationRepository = medicationRepository;
            this.mapper=mapper;
        }

        [HttpGet]
        public IActionResult GetAllPrescriptions()
        {
            var prescriptions = prescriptionRepository.GetAll().Result;
            var prescriptionsDto = mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions);
            return Ok(prescriptionsDto);
        }

        [HttpGet("{prescriptionId:Guid}/medicationsDosages")]
        public IActionResult GetAllMedicationsFromPrescription(Guid prescriptionId)
        {
            var prescription = prescriptionRepository.GetById(prescriptionId).Result;
            if (prescription == null)
            {
                return NotFound("Prescription with given id not found");
            }

            var medications = prescription.MedicationDosagePrescriptions;
            return Ok(medications);
        }


        [HttpPost]
        public IActionResult CreatePrescription([FromBody] CreatePrescriptionDto prescriptionDto)
        {

            var prescription = new Prescription();
            
            var doctor = doctorRepository.GetById(prescriptionDto.DoctorId).Result;
            var patient = patientRepository.GetById(prescriptionDto.PacientId).Result;

            if(doctor == null)
            {
                return BadRequest("There is no doctor with given id");
            }
            if(patient == null)
            {
                return BadRequest("There is no patient with given id");
            }
            doctor.AddPrescription(prescription);
            patient.AddPrescription(prescription);

            var medicationDosages = new List<MedicationDosagePrescription>();
            foreach (var medicationDosageDto in prescriptionDto.MedicationDosages)
            {
                var medication = medicationRepository.GetById(medicationDosageDto.MedicationId).Result;

                if(medication == null)
                {
                    return BadRequest("Medication with given id not found");
                }

                var medicationDosage = new MedicationDosagePrescription(medicationDosageDto.StartDate, medicationDosageDto.EndDate, medicationDosageDto.Quantity, medicationDosageDto.Frequency);
                medicationDosage.AddMedication(medication);
                medicationDosages.Add(medicationDosage);
                
            }

            prescription.AddMedications(medicationDosages);

            
            prescriptionRepository.Add(prescription);
            prescriptionRepository.SaveChanges();
            return Created(nameof(GetAllPrescriptions), prescription);
        }


        //[HttpPost("{prescriptionId:Guid}/medications")]
        //public IActionResult RegisterNewMedicationsToPrescription(Guid prescriptionId, [FromBody] List<CreateMedicationDto> medicationDtos)
        //{

        //    var prescription = prescriptionRepository.GetById(prescriptionId);
        //    if (prescription == null)
        //    {
        //        return NotFound("Prescription with given id not found");
        //    }

        //    var medications = medicationDtos.Select(d => new Medication(d.Name, d.Stock)).ToList();
        //    var result = prescription.AddMedications(medications);


        //    prescriptionRepository.SaveChanges();

        //    return Ok(result);

        //}

        [HttpDelete("{prescriptionId:Guid}")]
        public IActionResult DeletePrescription(Guid prescriptionId)
        {
            prescriptionRepository.Delete(prescriptionId);
            prescriptionRepository.SaveChanges();
            return NoContent();
        }
    }
}
