using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public readonly IRepository<Patient> patientRepository;
        public readonly IRepository<Doctor> doctorRepository;
        public PatientsController(IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
        }
        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = patientRepository.GetAll().Select(
                p => new PatientDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Phone = p.Phone
                }) ;
            return Ok(patients);
        }

        
        [HttpGet("{patientId:Guid}/doctors")]
        public IActionResult GetAllDoctorsFromPatient(Guid patientId)
        {
            var doctors = doctorRepository.Find(doctor => doctor.Patients.Contains(patientRepository.GetById(patientId))).ToList();

            return Ok(doctors.Select(
                d => new DoctorDto
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization,
                    Email = d.Email,
                    Phone = d.Phone,
                }));

        }

        [HttpPost]
        public IActionResult CreatePatient([FromBody] CreatePatientDto patientDto)
        {
            var patient = new Patient(patientDto.FirstName, patientDto.LastName, patientDto.Email, patientDto.Phone);
            patientRepository.Add(patient);
            patientRepository.SaveChanges();
            return Created(nameof(GetAllPatients), patient);
        }

        
        [HttpPost("{patientId:Guid}/doctors")]
        public IActionResult RegisterNewDoctorsToPatient(Guid patientId, [FromBody] List<CreateDoctorDto> doctorsDtos)
        {

            var patient = patientRepository.GetById(patientId);
            if (patient == null)
            {
                return NotFound("Patient with given id not found");
            }

            var doctors = doctorsDtos.Select(d => new Doctor(d.FirstName, d.LastName, d.Specialization, d.Email, d.Phone)).ToList();
            var result = patient.AddDoctors(doctors);


            return Ok(doctors);
           
        }

        [HttpDelete("{patientId:Guid}")]
        public IActionResult DeletePatient(Guid patientId)
        {
            patientRepository.Delete(patientId);
            patientRepository.SaveChanges();
            return NoContent();
        }
    }
}
