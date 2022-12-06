using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Data
{
    public class MyDocAppointmentDatabaseContext : DbContext, IDatabaseContext
    {

        //public MyDocAppointmentDatabaseContext(DbContextOptions<TestsDatabaseContext> options) : base(options) 
        //{
        //                this.Database.EnsureCreated();

        //}

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

        public DbSet<Event> Events => Set<Event>();
        public DbSet<MedicationDosagePrescription> MedicationDosagePrescriptions => Set<MedicationDosagePrescription>();
        public DbSet<MedicationDosageHistory> MedicationDosageHistories => Set<MedicationDosageHistory>();

        public void Save()
        {
            SaveChanges();
        }

        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<History> Histories => Set<History>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = dbMyDocAppointmentManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set null hospitalId on doctor when delete hospital
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
