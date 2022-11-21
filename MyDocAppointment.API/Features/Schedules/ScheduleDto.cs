using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.API.Features.Schedules
{
    public class ScheduleDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
