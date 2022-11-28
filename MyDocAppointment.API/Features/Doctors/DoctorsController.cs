using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Doctors
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Doctor> doctorRepository;

        public DoctorsController(IRepository<Doctor> doctorRepository)
        {
            this.doctorRepository = doctorRepository;
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto doctorDto)
        {
            var doctor = new Doctor(doctorDto.FirstName, doctorDto.LastName, doctorDto.Specialization,
                doctorDto.Email, doctorDto.Phone, doctorDto.Title, doctorDto.Profession, doctorDto.Location, doctorDto.Grade,doctorDto.Reviews);

            doctorRepository.Add(doctor);
            doctorRepository.SaveChanges();
            return Created(nameof(GetAllDoctors), doctor);
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
