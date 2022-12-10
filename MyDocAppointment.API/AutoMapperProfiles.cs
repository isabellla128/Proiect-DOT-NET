using AutoMapper;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.API
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}
