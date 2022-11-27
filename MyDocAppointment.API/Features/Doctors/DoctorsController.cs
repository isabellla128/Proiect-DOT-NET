using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Doctors
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<Patient> patientRepositroy;

        public DoctorsController(IRepository<Doctor> doctorRepository, IRepository<Appointment> appointmentRepository, IRepository<Patient> patientRepositroy)
        {
            this.doctorRepository = doctorRepository;
            this.appointmentRepository = appointmentRepository;
            this.patientRepositroy = patientRepositroy;
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = doctorRepository.GetAll().Select
            (
                d => new DoctorDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization,
                    Email = d.Email,
                    Phone = d.Phone,
                }
             );
            return Ok(doctors);

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto doctorDto)
        {
            var doctor = new Doctor(doctorDto.FirstName, doctorDto.LastName, doctorDto.Specialization,
                doctorDto.Email, doctorDto.Phone);

            doctorRepository.Add(doctor);
            doctorRepository.SaveChanges();
            return Created(nameof(GetAllDoctors), doctor);
        }

        [HttpPost("{doctorId:Guid}/appointments")]
        public IActionResult AddAppointmentsToDoctor(Guid doctorId, [FromBody] List<AppointmentsDtoFromDoctor> appointmentDtos)
        {

            var doctor = doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                return NotFound("Doctor with given id not found");
            }
            var appointments = new List<Appointment>();
            foreach (var a in appointmentDtos)
            {
                var appointment = new Appointment(a.StartTime, a.EndTime);

                var patient = patientRepositroy.GetById(a.PatientId);
                if(patient == null)
                {
                    return BadRequest($"Patient with given id ({a.PatientId}) not found");
                }
                var resultFromPatient = patient.AddAppointment(appointment);
                if(resultFromPatient.IsFailure) 
                {
                    return BadRequest(resultFromPatient.Error);
                }
                var resultFromDoctor = doctor.AddAppointment(appointment);
                if(resultFromDoctor.IsFailure)
                {
                    return BadRequest(resultFromDoctor.Error);
                }
                appointments.Add(appointment);
            }


            appointments.ForEach(a =>
            {
                appointmentRepository.Add(a);
            });
            appointmentRepository.SaveChanges();

            return Ok(appointments);

        }

        [HttpDelete("{doctorId:Guid}")]
        public IActionResult DeleteHospital(Guid doctorId)
        {
            try
            {
                doctorRepository.Delete(doctorId);
            }
            catch(ArgumentException e)
            {
                return NotFound(e.Message);
            }
            doctorRepository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{personId:Guid}")]
        public IActionResult changeHospital()
        {
            return Ok();
        }
    }
}
