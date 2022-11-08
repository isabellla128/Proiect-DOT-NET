namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {
        public Prescription()
        {

        }

        public Prescription(Doctor doctor, Patient patient, ICollection<Medication> medications)
        {
            Doctor = doctor;
            Patient = patient;
            Medications = medications;
        }
        public int Id { get; private set; }

        public Doctor Doctor { get; private set; }

        public Patient Patient { get; private set; }

        public ICollection<Medication> Medications { get; set; }
    }
}
