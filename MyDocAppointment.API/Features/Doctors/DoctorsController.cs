using Microsoft.AspNetCore.Mvc;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories;

namespace MyDocAppointment.API.Features.Doctors
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly Repository<Doctor> doctorRepository;

        public DoctorsController(Repository<Doctor> doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        
    }
}
