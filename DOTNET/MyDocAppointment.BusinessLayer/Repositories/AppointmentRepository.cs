using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class AppointmentRepository : Repository<Appointment>
    {
        public AppointmentRepository(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
