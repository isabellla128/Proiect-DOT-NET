namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {
        public Prescription()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }

        public Doctor Doctor { get; private set; }

        public int DoctorId { get; private set; }

        public Patient Patient { get; private set; }

        public int PatientId { get; private set; }

        public ICollection<Medication> Medications { get; private set; }
    }
}
