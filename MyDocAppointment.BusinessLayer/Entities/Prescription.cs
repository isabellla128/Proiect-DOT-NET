namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {

        public Prescription(ICollection<Medication> medications)
        {
            Medications = medications;
        }

        //todo add doctor and pacient
        public int Id { get; private set; }

        public Doctor Doctor { get; private set; }

        public Patient Patient { get; private set; }

        public ICollection<Medication> Medications { get; set; }
    }
}
