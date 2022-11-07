using Microsoft.EntityFrameworkCore;

namespace MyDocAppointment.BusinessLayer.Data
{
    public class MyDocAppointmentDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyDocAppointmentManagement.db");
        }
    }
}
