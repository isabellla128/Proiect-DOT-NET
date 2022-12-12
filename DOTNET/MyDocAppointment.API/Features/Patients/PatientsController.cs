using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Appointments;
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
        private readonly IMapper mapper;

        public PatientsController(IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository, IRepository<Appointment> appointmentRepository, IMapper mapper)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentRepository = appointmentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = patientRepository.GetAll().Result;
            var patientsDto = mapper.Map<IEnumerable<PatientDto>>(patients);
            return Ok(patientsDto);
        }

        [HttpGet("{patientId:Guid}/appointments")]
        public IActionResult GetAllDoctorsFromPatient(Guid patientId)
        {

            var appointments = appointmentRepository.Find(appointment => appointment.PatientId == patientId).Result;

            if(!appointments.Any())
            {
                return NotFound("There is no patient with given id");
            }

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

            if (patientDto.FirstName != null && patientDto.LastName != null && patientDto.Email != null && patientDto.Phone != null)
            {
                var patient = new Patient(patientDto.FirstName, patientDto.LastName, patientDto.Email, patientDto.Phone);
                patientRepository.Add(patient);
                patientRepository.SaveChanges();
                return Created(nameof(GetAllPatients), patient);
            }
            return BadRequest("The fields in patient must not be null");
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
