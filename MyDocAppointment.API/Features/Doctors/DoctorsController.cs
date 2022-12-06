using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Numerics;

namespace MyDocAppointment.API.Features.Doctors
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepositroy;

        public DoctorsController(IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IPatientRepository patientRepositroy)
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
                    Title = d.Title,
                    Profession = d.Profession,
                    Location= d.Location,
                    Grade= d.Grade,
                    Reviews= d.Reviews,
                }
             );
            return Ok(doctors);
        }

        [HttpGet("{doctorId:Guid}/appointments")]
        public IActionResult GetAppointmentsFromDoctor(Guid doctorId)
        {
            var appointments = appointmentRepository.Find(appointment => appointment.DoctorId == doctorId);
            return Ok(appointments.Select(
                a => new AppointmentsDtoFromDoctor
                {
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    PatientId = a.PatientId,
                })) ;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto doctorDto)
        {
            var doctor = new Doctor(doctorDto.FirstName, doctorDto.LastName, doctorDto.Specialization,
                doctorDto.Email, doctorDto.Phone, doctorDto.Title, doctorDto.Profession, doctorDto.Location, doctorDto.Grade,doctorDto.Reviews);

            doctorRepository.Add(doctor);
            doctorRepository.SaveChanges();
            return Created(nameof(GetAllDoctors), doctor);
        }

        [HttpPost("{doctorId:Guid}/appointments")]
        public IActionResult RegisterNewDoctorsToPatient(Guid doctorId, [FromBody] List<AppointmentsDtoFromDoctor> appointmentDtos)
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

        [HttpPut("{doctorId:Guid}")]
        public IActionResult UpdateDoctor(Guid doctorId, [FromBody] Doctor doctor)
        {
            var doctorToChange = doctorRepository.GetById(doctorId);

            doctorToChange.UpdateDoctor(doctor);

            doctorRepository.SaveChanges();
            return Ok(doctorToChange);
        }

    }
}
