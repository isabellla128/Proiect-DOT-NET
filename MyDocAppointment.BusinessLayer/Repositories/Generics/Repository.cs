using System.Linq.Expressions;
using MyDocAppointment.BusinessLayer.Data;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected TestsDatabaseContext context;

        public Repository(TestsDatabaseContext context)
        {
            this.context = context;
        }

        public virtual T Add(T entity)
        {
            context.Add(entity);
            //context.SaveChanges();
            return entity;
        }

        public virtual T Delete(Guid id)
        {
            var entity = context.Find<T>(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no {typeof(T).Name} with id = {id}");
            }
            context.Remove(entity);
            //context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public virtual T? GetById(Guid id)
        {
            return context.Find<T>(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            context.Update(entity);
            //context.SaveChanges();
            return entity;
        }
    }
}
