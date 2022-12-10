using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IEventRepositrory
    {
        Event Add(Event entity);
        Event Delete(Guid id);
        IEnumerable<Event> Find(Expression<Func<Event, bool>> predicate);
        IEnumerable<Event> GetAll();
        Event? GetById(Guid id);
        void SaveChanges();
        Event Update(Event entity);
    }
}