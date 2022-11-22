using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.API.Features.Appointments
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
