using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Data;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected MyDocAppointmentDatabaseContext context;

        public Repository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            await context.AddAsync(entity);
            //context.SaveChanges();
            return entity;
        }

        public virtual async Task<T?> Delete(Guid id)
        {
            var entity = await context.FindAsync<T>(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no {typeof(T).Name} with id = {id}");
            }
            context.Remove(entity);
            //context.SaveChanges();
            return entity;
        }

        public virtual async Task<IReadOnlyCollection<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToListAsync();
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        public async void SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            context.Update(entity);
            //context.SaveChanges();
            return entity;
        }
    }
}
