using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Histories;
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
        private readonly IMapper mapper;

        public HospitalsController(IRepository<Hospital> hospitalRepository, IRepository<Doctor> doctorRepository,IMapper mapper)
        {
            this.hospitalRepository = hospitalRepository;
            this.doctorRepository = doctorRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllHospitals()
        {
            var hospitals = hospitalRepository.GetAll();
            var hospitalsDto= mapper.Map<IEnumerable<HospitalDto>>(hospitals);

            return Ok(hospitalsDto);
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
                    Id = d.Id
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

            var doctors = doctorsDtos.Select(d => new Doctor(d.FirstName, d.LastName, d.Specialization, d.Email, d.Phone, d.Title, d.Profession, d.Location, d.Grade, d.Reviews)).ToList();

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
