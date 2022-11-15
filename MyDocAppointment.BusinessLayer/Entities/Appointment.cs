using System.Xml.Serialization;
using ShelterManagement.Business.Helpers;

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
        //todo add doctor and patient

        public Guid Id { get; private set; }
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void AddDoctorToPrescription(Doctor doctor)
        {
            Doctor = doctor;
            DoctorId = doctor.Id;
        }

        public void AddPatientToPrescription(Patient patient)
        {
            Patient = patient;
            PatientId = patient.Id;
        }

    }
}
