using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Prescriptions
{
    [Route("api/[controller]")]
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

        [HttpDelete("{prescriptionId:Guid}")]
        public IActionResult DeletePrescription(Guid prescriptionId)
        {
            prescriptionRepository.Delete(prescriptionId);
            prescriptionRepository.SaveChanges();
            return NoContent();
        }
    }
}
