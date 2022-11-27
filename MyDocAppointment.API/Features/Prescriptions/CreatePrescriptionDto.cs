namespace MyDocAppointment.API.Features.Prescriptions
{
    public class CreatePrescriptionDto
    {
        public Guid DoctorId { get; set; }
        public Guid PacientId { get; set; }
    }
}
