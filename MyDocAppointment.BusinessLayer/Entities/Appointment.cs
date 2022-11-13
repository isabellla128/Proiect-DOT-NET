namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Appointment
    {

        public Appointment(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        //todo add doctor and patient

        public int Id { get; private set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

    }
}
