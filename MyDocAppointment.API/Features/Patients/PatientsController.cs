using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Patients
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public readonly IRepository<Patient> patientRepository;
        public readonly IRepository<Doctor> doctorRepository;
        public readonly IRepository<Appointment> appointmentRepository;
        public PatientsController(IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository, IRepository<Appointment> appointmentRepository)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentRepository = appointmentRepository;
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

        [HttpGet("{patientId:Guid}/appointments")]
        public IActionResult GetAllDoctorsFromPatient(Guid patientId)
        {
            /*var patient = patientRepository.GetById(patientId);
            if (patient == null)
            {
                return NotFound("Patient with given id not found");
            }

            var appointments = patient.Appointments;
            return Ok(appointments);*/
            var appointments = appointmentRepository.Find(appointment => appointment.PatientId == patientId);
            return Ok(appointments.Select(
                a => new AppointmentsDtoFromPatient
                {
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    DoctorId = a.DoctorId,
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


        [HttpDelete("{patientId:Guid}")]
        public IActionResult DeletePatient(Guid patientId)
        {
            patientRepository.Delete(patientId);
            patientRepository.SaveChanges();
            return NoContent();
        }

    }
}
