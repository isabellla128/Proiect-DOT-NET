namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {
        public Prescription()
        {
            Id = Guid.NewGuid();
            Medications = new List<Medication>();
        }
        public Guid Id { get; private set; }

        public Doctor Doctor { get; private set; }

        public Guid DoctorId { get; private set; }

        public Patient Patient { get; private set; }

        public Guid PatientId { get; private set; }

        public ICollection<Medication> Medications { get; private set; }

        
        public void AddDoctorToPrescription(Doctor doctor)
        {
            Doctor = doctor;
        }
        public void AddPatientToPrescription(Patient patient)
        {
            Patient = patient;
        }
    }
}
