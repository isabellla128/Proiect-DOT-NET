using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class ScheduleRepository : Repository<Schedule>
    {
        public ScheduleRepository(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
