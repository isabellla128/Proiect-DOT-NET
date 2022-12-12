﻿using AutoMapper;
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
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, AppointmentsDtoFromDoctor>().ReverseMap();


            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Doctor, CreateDoctorDto>().ReverseMap();

            CreateMap<History, HistoryDto>().ReverseMap();
            CreateMap<History, HistoryDto>().ReverseMap();

            CreateMap<Hospital, HospitalDto>().ReverseMap();
            CreateMap<Hospital, HospitalDto>().ReverseMap();


            CreateMap<Medication, MedicationDto>().ReverseMap();
            CreateMap<Medication, CreateMedicationDto>().ReverseMap();


            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Patient, CreatePatientDto>().ReverseMap();

            CreateMap<Prescription, PrescriptionDto>().ReverseMap();
            CreateMap<Prescription, CreatePrescriptionDto>().ReverseMap();

        }
    }
}
