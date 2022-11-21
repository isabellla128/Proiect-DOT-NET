namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Schedule
    {
        public Schedule(DateTime startDate, DateTime endDate, ICollection<Event> events)
        {
            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            Events = events;
        }

        public Guid Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public ICollection<Event> Events { get; private set; }

        public bool IsEndDateValid() => DateTime.Now > EndDate;

        public void AddEvent(Event @event)
        {
            Events.Add(@event);
        }

    }
}
