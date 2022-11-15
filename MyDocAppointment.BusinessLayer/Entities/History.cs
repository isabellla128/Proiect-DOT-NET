namespace MyDocAppointment.BusinessLayer.Entities
{
    public class History
    {
        public History(DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Id { get; private set; }

        public ICollection<Medication> Medications { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public bool IsStartDateValid() => DateTime.Now < StartDate;

    }
}
