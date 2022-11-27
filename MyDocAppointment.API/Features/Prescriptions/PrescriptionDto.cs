namespace MyDocAppointment.API.Features.Prescriptions
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }  
        public Guid DoctorId { get; set; }
        public Guid PacientId { get; set; }
    }
}
