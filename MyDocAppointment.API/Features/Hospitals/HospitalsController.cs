using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Hospitals
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IRepository<Hospital> hospitalRepository;

        public HospitalsController(IRepository<Hospital> hospitalRepository)
        {
            this.hospitalRepository = hospitalRepository;
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

        [HttpPost]
        public IActionResult CreateHospital([FromBody] CreateHospitalDto hospitalDto)
        {
            var hospital = new Hospital(hospitalDto.Name, hospitalDto.Address, hospitalDto.Phone);
            hospitalRepository.Add(hospital);
            hospitalRepository.SaveChanges();
            return Created(nameof(GetAllHospitals), hospital);
        }
    }
}
