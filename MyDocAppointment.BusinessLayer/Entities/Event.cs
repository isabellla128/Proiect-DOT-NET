namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Event
    {
        public Event(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public bool ValidateName()
        {
            return string.IsNullOrWhiteSpace(Name);
        }

        public bool IsStartDateValid() => DateTime.Now > StartDate;

    }
}
