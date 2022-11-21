using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Hospitals
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IRepository<Hospital> hospitalRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public HospitalsController(IRepository<Hospital> hospitalRepository, IRepository<Doctor> doctorRepository)
        {
            this.hospitalRepository = hospitalRepository;
            this.doctorRepository = doctorRepository;
        }

        [HttpGet]
        public IActionResult GetAllHospitals()
        {
            var hospitals = hospitalRepository.GetAll().Select(
                    h => new HospitalDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Address = h.Address,
                        Phone = h.Phone
                    }
                );

            return Ok(hospitals);
        }

        [HttpGet("{hospitalId:Guid}/doctors")]
        public IActionResult GetAllDoctorsFromHostpital(Guid hospitalId)
        {
            var doctors = doctorRepository.Find(doctor => doctor.HospitalId == hospitalId);
            return Ok(doctors.Select(
                d => new DoctorDto
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization,
                    Email = d.Email,
                    Phone = d.Phone,
                    //HospitalId = d.HospitalId
                }));

        }

        [HttpPost]
        public IActionResult CreateHospital([FromBody] CreateHospitalDto hospitalDto)
        {
            var hospital = new Hospital(hospitalDto.Name, hospitalDto.Address, hospitalDto.Phone);
            hospitalRepository.Add(hospital);
            hospitalRepository.SaveChanges();
            return Created(nameof(GetAllHospitals), hospital);
        }

        [HttpPost("{hospitalId:Guid}/doctors")]
        public IActionResult RegisterNewDoctorsToHospital(Guid hospitalId, [FromBody] List<CreateDoctorDto> doctorsDtos)
        {

            var hospital = hospitalRepository.GetById(hospitalId);
            if (hospital == null)
            {
                return NotFound("Hospital with given id not found");
            }

            var doctors = doctorsDtos.Select(d => new Doctor(d.FirstName, d.LastName, d.Specialization, d.Email, d.Phone)).ToList();

            var result = hospital.AddDoctors(doctors);


            doctors.ForEach(d =>
            {
                doctorRepository.Add(d);
            });
            doctorRepository.SaveChanges();

            return result.IsSuccess ? NoContent() : BadRequest();
        }

        [HttpDelete("{hospitalId:Guid}")]
        public IActionResult DeleteHospital(Guid hospitalId)
        {
            hospitalRepository.Delete(hospitalId);
            hospitalRepository.SaveChanges();

            return NoContent();
        }

        
        
    }
}
