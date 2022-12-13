using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.API.Features.Appointments
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public Guid doctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public Guid patientId { get; set; }
        public Patient? Patient { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
