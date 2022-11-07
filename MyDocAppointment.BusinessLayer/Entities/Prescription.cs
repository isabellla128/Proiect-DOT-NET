namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Prescription
    {
        public Prescription()
        {

        }

        public Prescription(int id_doctor, int id_pacient, ICollection<Medication> medications)
        {
            Id_doctor = id_doctor;
            Id_pacient = id_pacient;
            Medications = medications;
        }
        public int Id { get; set; }

        public int Id_doctor { get; set; }

        public int Id_pacient { get; set; }

        public ICollection<Medication> Medications { get; set; }
    }
}
