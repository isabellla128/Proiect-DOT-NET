namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Event
    {
        public Event(string name, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Schedule Schedule { get; private set; }
        public Guid ScheduleId {  get; private set; }

        public bool ValidateName()
        {
            return string.IsNullOrWhiteSpace(Name);
        }

        public bool IsStartDateValid() => DateTime.Now > StartDate;

        public void AddScheduleToEvent(Schedule schedule)
        {
            this.Schedule = schedule;
            ScheduleId = schedule.Id;
        }

    }
}
