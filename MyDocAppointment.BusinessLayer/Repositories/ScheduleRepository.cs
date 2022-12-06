using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        protected IDatabaseContext context;

        public ScheduleRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public Schedule Add(Schedule Schedule)
        {

            context.Schedules.Add(Schedule);
            return Schedule;
        }

        public Schedule Delete(Guid id)
        {
            var entity = context.Schedules.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Schedule with id = {id}");
            }
            context.Schedules.Remove(entity);
            return entity;
        }

        public IEnumerable<Schedule> Find(Expression<Func<Schedule, bool>> predicate)
        {
            return context.Schedules
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Schedule> GetAll()
        {
            return context.Schedules.ToList();
        }

        public virtual Schedule? GetById(Guid id)
        {
            return context.Schedules.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Schedule Update(Schedule entity)
        {
            context.Schedules.Update(entity);
            return entity;
        }
    }
}
