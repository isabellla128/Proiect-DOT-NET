namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Appointment
    {

        public Appointment(DateTime startTime, DateTime endTime)
        {
            Id = Guid.NewGuid();
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; private set; }
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void AddDoctorToAppointment(Doctor doctor)
        {
            Doctor = doctor;
            DoctorId = doctor.Id;
        }

        public void AddPatientToAppointment(Patient patient)
        {
            Patient = patient;
            PatientId = patient.Id;
        }

    }
}
