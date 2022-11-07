using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Data
{
    public class MyDocAppointmentDatabaseContext : DbContext
    {
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyDocAppointmentManagement.db");
        }
    }
}
