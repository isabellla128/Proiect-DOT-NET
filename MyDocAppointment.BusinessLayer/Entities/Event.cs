namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Event
    {
        public Event(int id, string name, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool ValidateName()
        {
            return string.IsNullOrWhiteSpace(Name);
        }

        public bool IsStartDateValid() => DateTime.Now > StartDate;

    }
}
