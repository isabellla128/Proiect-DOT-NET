namespace MyDocAppointment.BusinessLayer.Entities
{
    public class History
    {
        public History(int id, Medication medication, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Medication = medication;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get; set; }
        public Medication Medication { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool isStartDateValid() => DateTime.Now < StartDate;

    }
}
