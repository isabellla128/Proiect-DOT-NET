﻿using AutoMapper;
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
        private readonly IMapper mapper;

        public DoctorsController(IRepository<Doctor> doctorRepository, IRepository<Appointment> appointmentRepository, IRepository<Patient> patientRepositroy, IMapper mapper)
        {
            this.doctorRepository = doctorRepository;
            this.appointmentRepository = appointmentRepository;
            this.patientRepositroy = patientRepositroy;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = doctorRepository.GetAll().Result;
            var doctorsDto = mapper.Map<IEnumerable<DoctorDto>>(doctors);

            return Ok(doctorsDto);
        }

        [HttpGet("{doctorId:Guid}/appointments")]
        public IActionResult GetAppointmentsFromDoctor(Guid doctorId)
        {
            var appointments = appointmentRepository.Find(appointment => appointment.DoctorId == doctorId).Result;
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
            var doctor = doctorRepository.GetById(doctorId).Result;
            if (doctor == null)
            {
                return NotFound("Doctor with given id not found");
            }
            var appointments = new List<Appointment>();
            foreach (var a in appointmentDtos)
            {
                var appointment = new Appointment(a.StartTime, a.EndTime);

                var patient = patientRepositroy.GetById(a.PatientId).Result;
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


        [HttpPut("{doctorId:Guid}")]
        public IActionResult UpdateDoctor(Guid doctorId, [FromBody] Doctor doctor)
        {
            var doctorToChange = doctorRepository.GetById(doctorId).Result;

            doctorToChange.UpdateDoctor(doctor);

            doctorRepository.SaveChanges();
            return Ok(doctorToChange);
        }

    }
}