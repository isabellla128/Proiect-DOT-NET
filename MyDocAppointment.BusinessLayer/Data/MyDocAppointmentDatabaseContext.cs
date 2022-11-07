using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Data
{
    public class MyDocAppointmentDatabaseContext : DbContext
    {
        public MyDocAppointmentDatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyDocAppointmentManagement.db");
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Medication> Medications { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

    }
}
