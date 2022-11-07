namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Schedule
    {
        public Schedule(int id, DateTime startDate, DateTime endDate, ICollection<Event> events)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Events = events;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Event> Events { get; set; }

        public bool IsEndDateValid() => DateTime.Now > EndDate;

        public void AddEvent(Event @event)
        {
            Events.Add(@event);
        }

    }
}
