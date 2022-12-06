using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocEvent.BusinessLayer.Repositories
{
    public class EventRepositrory : IEventRepositrory
    {
        protected IDatabaseContext context;

        public EventRepositrory(IDatabaseContext context)
        {
            this.context = context;
        }

        public Event Add(Event entity)
        {
            context.Events.Add(entity);
            return entity;
        }

        public Event Delete(Guid id)
        {
            var entity = context.Events.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Event with id = {id}");
            }
            context.Events.Remove(entity);
            return entity;
        }

        public IEnumerable<Event> Find(Expression<Func<Event, bool>> predicate)
        {
            return context.Events
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Event> GetAll()
        {
            return context.Events.ToList();
        }

        public virtual Event? GetById(Guid id)
        {
            return context.Events.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Event Update(Event entity)
        {
            context.Events.Update(entity);
            return entity;
        }
    }
}
