using ShelterManagement.Business.Helpers;

namespace MyDocAppointment.BusinessLayer.Entities
{
    public class MedicationDosagePrescription : MedicationDosage
    {
        public MedicationDosagePrescription(DateTime startDate, DateTime endDate, float quantity, float frequency) : base(startDate, endDate, quantity, frequency)
        {
        }

        public Prescription? Prescription { get; private set; }

        public Guid PrescriptionId { get; private set; }

        public Result RegisterMedicationInfoToPrescription(Prescription? prescription)
        {
            if(prescription == null)
            {
                return Result.Failure("Prescription should not be null");
            }

            Prescription = prescription;
            PrescriptionId = prescription.Id;
            return Result.Success();
        }
    }
}
