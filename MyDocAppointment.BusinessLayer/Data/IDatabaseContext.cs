using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Data
{
    public interface IDatabaseContext
    {
        DbSet<Appointment> Appointments { get; }
        DbSet<Doctor> Doctors { get; }
        DbSet<Event> Events { get; }
        DbSet<Hospital> Hospitals { get; }
        DbSet<MedicationDosageHistory> MedicationDosageHistories { get; }
        DbSet<MedicationDosagePrescription> MedicationDosagePrescriptions { get; }
        DbSet<Medication> Medications { get; }
        DbSet<Patient> Patients { get; }
        DbSet<Prescription> Prescriptions { get; }
        DbSet<Schedule> Schedules { get; }
        DbSet<History> Histories { get; }

        void Save();
    }
}