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
        public PrescriptionsController(IRepository<Prescription> prescriptionRepository, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
        {
            this.prescriptionRepository=prescriptionRepository;
            this.doctorRepository=doctorRepository;
            this.patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult GetAllPrescriptions()
        {
            var prescriptions = prescriptionRepository.GetAll().Select(
                p => new PrescriptionDto
                {
                    Id = p.Id
                });
            return Ok(prescriptions);
        }

        [HttpGet("{prescriptionId:Guid}/medications")]
        public IActionResult GetAllMedicationsFromPrescription(Guid prescriptionId)
        {
            var prescription = prescriptionRepository.GetById(prescriptionId);
            if (prescription == null)
            {
                return NotFound("Prescription with given id not found");
            }

            var medications = prescription.Medications;
            return Ok(medications);
        }


        [HttpPost]
        public IActionResult CreatePrescription(Guid doctorId , Guid patientId, [FromBody] CreatePrescriptionDto prescriptionDto)
        {

            var prescription = new Prescription();
            
            var doctor = doctorRepository.GetById(doctorId);
            var patient = patientRepository.GetById(patientId);

            prescription.AddDoctorToPrescription(doctor);
            prescription.AddPatientToPrescription(patient);
            
            prescriptionRepository.Add(prescription);
            prescriptionRepository.SaveChanges();
            return Created(nameof(GetAllPrescriptions), prescription);
        }


        [HttpPost("{prescriptionId:Guid}/medications")]
        public IActionResult RegisterNewMedicationsToPrescription(Guid prescriptionId, [FromBody] List<CreateMedicationDto> medicationDtos)
        {

            var prescription = prescriptionRepository.GetById(prescriptionId);
            if (prescription == null)
            {
                return NotFound("Prescription with given id not found");
            }

            var medications = medicationDtos.Select(d => new Medication(d.Name, d.Stock)).ToList();
            var result = prescription.AddMedications(medications);


            prescriptionRepository.SaveChanges();

            return Ok(result);

        }

        [HttpDelete("{prescriptionId:Guid}")]
        public IActionResult DeletePrescription(Guid prescriptionId)
        {
            prescriptionRepository.Delete(prescriptionId);
            prescriptionRepository.SaveChanges();
            return NoContent();
        }
    }
}
