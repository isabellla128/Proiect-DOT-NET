using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Events;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Appointments
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Doctor> doctorRepository;


        public AppointmentsController(IRepository<Appointment> appointmentRepository, IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository; 
            this.doctorRepository = doctorRepository;
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            var appointments = appointmentRepository.GetAll().Select
            (
                a => new AppointmentDto
                {
                    Id = a.Id,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                }
             );
            return Ok(appointments);

        }

        [HttpPost]
        public IActionResult Create(Guid doctorId, Guid patientId,[FromBody] CreateAppointmentDto appointmentDto)
        {
            var appointment = new Appointment(appointmentDto.StartTime, appointmentDto.EndTime);
            var doctor = doctorRepository.GetById(doctorId);
            var patient = patientRepository.GetById(patientId);
            if(doctor == null)
            {
                return BadRequest("Doctor with given id not found");
            }
            if(patient == null)
            {
                return BadRequest("Patient with given id not found");
            }
            appointment.AddDoctorToAppointment(doctor);
            appointment.AddPatientToAppointment(patient);

            appointmentRepository.Add(appointment);
            appointmentRepository.SaveChanges();
            return Created(nameof(GetAllAppointments), appointment);
        }

        [HttpDelete("{appointmentId:Guid}")]
        public IActionResult DeleteAppointment(Guid appointmentId)
        {
            try
            {
                appointmentRepository.Delete(appointmentId);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            appointmentRepository.SaveChanges();

            return NoContent();
        }
    }
}
