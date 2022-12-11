namespace MyDocAppointment.API.Features.Events
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
