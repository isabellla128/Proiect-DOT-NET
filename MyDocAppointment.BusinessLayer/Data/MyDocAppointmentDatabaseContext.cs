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
        public DbSet<Hospital> Hospitals => Set<Hospital>();

        public DbSet<Appointment> Appointments => Set<Appointment>();

        public DbSet<Patient> Patients => Set<Patient>();

        public DbSet<Doctor> Doctors => Set<Doctor>();

        public DbSet<Medication> Medications => Set<Medication>();

        public DbSet<Prescription> Prescriptions => Set<Prescription>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = dbMyDocAppointmentManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Hospial)
                .WithMany(h => h.Doctors)
                .HasForeignKey(@"HospitalId")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
