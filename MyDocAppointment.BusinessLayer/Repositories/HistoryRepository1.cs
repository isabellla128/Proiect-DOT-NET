using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class HistoryRepository1 : IHistoryRepository1
    {
        protected IDatabaseContext context;

        public HistoryRepository1(IDatabaseContext context)
        {
            this.context = context;
        }

        public History Add(History history)
        {

            context.Histories.Add(history);
            context.Histories.Add(history);
            return history;
        }

        public History Delete(Guid id)
        {
            var entity = context.Histories.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no History with id = {id}");
            }
            context.Histories.Remove(entity);
            return entity;
        }

        public IEnumerable<History> Find(Expression<Func<History, bool>> predicate)
        {
            return context.Histories
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<History> GetAll()
        {
            return context.Histories.ToList();
        }

        public virtual History? GetById(Guid id)
        {
            return context.Histories.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual History Update(History entity)
        {
            context.Histories.Update(entity);
            return entity;
        }
    }
}
