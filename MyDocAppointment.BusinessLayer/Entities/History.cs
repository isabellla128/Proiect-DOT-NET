namespace MyDocAppointment.BusinessLayer.Entities
{
    public class History
    {
        public History()
        {

        }
        public History(Medication medication, DateTime startDate, DateTime endDate)
        {
            Medication = medication;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get; private set; }
        public Medication Medication { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public bool IsStartDateValid() => DateTime.Now < StartDate;

    }
}
