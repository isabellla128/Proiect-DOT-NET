using ShelterManagement.Business.Helpers;

namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Schedule
    {
        public Schedule(DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            Events = new List<Event>();
        }

        public Guid Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public ICollection<Event> Events { get; private set; }

        public bool IsEndDateValid() => DateTime.Now > EndDate;

        public Result AddEvents(List<Event> events)
        {
            if (!events.Any())
            {
                return Result.Failure("You must add at least an event");
            }
            events.ForEach(e =>
            {
                if (!Events.Contains(e))
                {
                    e.AddScheduleToEvent(this);
                    Events.Add(e);
                }
            });
            return Result.Success();
        }

    }
}
