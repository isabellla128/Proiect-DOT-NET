using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class EventRepositrory : Repository<Event>
    {
        public EventRepositrory(MyDocAppointmentDatabaseContext context) : base(context)
        {
        }
    }
}
