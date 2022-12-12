using AutoMapper;
using MyDocAppointment.API.Features.Appointments;
using MyDocAppointment.API.Features.Doctors;
using MyDocAppointment.API.Features.Histories;
using MyDocAppointment.API.Features.Hospitals;
using MyDocAppointment.API.Features.Medications;
using MyDocAppointment.API.Features.Patients;
using MyDocAppointment.API.Features.Prescriptions;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.API
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<History, HistoryDto>();
            CreateMap<Hospital, HospitalDto>();
            CreateMap<Medication, MedicationDto>();
            CreateMap<Patient, PatientDto>();
            CreateMap<Prescription, PrescriptionDto>();
        }
    }
}
