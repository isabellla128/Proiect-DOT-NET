using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class EventRepositrory : Repository<Event>
    {
        public EventRepositrory(TestsDatabaseContext context) : base(context)
        {
        }
    }
}
