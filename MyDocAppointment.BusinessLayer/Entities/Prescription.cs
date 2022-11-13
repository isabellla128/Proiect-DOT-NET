namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {


        //todo add doctor and pacient
        public int Id { get; private set; }

        public Doctor Doctor { get; private set; }

        public int DoctorId { get; set; }

        public Patient Patient { get; private set; }

        public int PatientId { get; set; }

        public ICollection<Medication> Medications { get; set; }
    }
}
