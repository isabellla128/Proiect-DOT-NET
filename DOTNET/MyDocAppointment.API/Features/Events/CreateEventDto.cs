namespace MyDocAppointment.API.Features.Events
{
    public class CreateEventDto
    {
        public Guid ScheduleId { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
