using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyDocAppointmentDatabaseContext context;

        public Repository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        public virtual T Add(T entity)
        {
            context.Add(entity);
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
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public virtual T? GetById(Guid id)
        {
            return context.Find<T>(id);
        }

        public virtual T Update(T entity)
        {
            context.Update(entity);
            return entity;
        }
    }
}
