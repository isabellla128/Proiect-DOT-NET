using ShelterManagement.Business.Helpers;

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

        public ICollection<MedicationDosageHistory> MedicationDosageHistories { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Patient Patient { get; private set; }

        public Guid PatientId { get; private set; }

        public bool IsStartDateValid() => DateTime.Now < StartDate;

        public void AddPatientToHistory(Patient patient)
        {
            this.Patient = patient;
            PatientId = patient.Id;
        }

        public Result AddMedications(List<MedicationDosageHistory> medicationsDosages)
        {
            medicationsDosages.ForEach(m =>
            {
                m.RegisterMedicationInfoToHistory(this);
                MedicationDosageHistories.Add(m);
            });
            return Result.Success();
        }

    }
}
