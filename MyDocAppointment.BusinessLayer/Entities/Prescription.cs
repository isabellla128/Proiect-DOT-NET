using ShelterManagement.Business.Helpers;

namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {
        public Prescription()
        {
            Id = Guid.NewGuid();
            MedicationDosagePrescriptions = new List<MedicationDosagePrescription>();
        }
        public Guid Id { get; private set; }

        public Doctor Doctor { get; private set; }

        public Guid DoctorId { get; private set; }

        public Patient Patient { get; private set; }

        public Guid PatientId { get; private set; }

        public ICollection<MedicationDosagePrescription> MedicationDosagePrescriptions { get; private set; }


        public void AddDoctorToPrescription(Doctor doctor)
        {
            Doctor = doctor;
            DoctorId = doctor.Id;
        }
        public void AddPatientToPrescription(Patient patient)
        {
            Patient = patient;
            PatientId = patient.Id;
        }
        public Result AddMedications(List<MedicationDosagePrescription> medicationsDosages)
        {
            medicationsDosages.ForEach(m =>
            {
                m.RegisterMedicationInfoToPrescription(this);
                MedicationDosagePrescriptions.Add(m);
            });
            return Result.Success();
        }
    }
}
