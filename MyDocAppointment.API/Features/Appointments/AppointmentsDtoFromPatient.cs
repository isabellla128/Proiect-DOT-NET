namespace MyDocAppointment.API.Features.Appointments
{
    public class AppointmentsDtoFromPatient
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid DoctorId { get; set; }
    }
}
