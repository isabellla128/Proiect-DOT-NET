namespace MyDocAppointment.API.Features.Appointments
{
    public class AppointmentsDtoFromDoctor
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Guid PatientId { get; set; }
    }
}
