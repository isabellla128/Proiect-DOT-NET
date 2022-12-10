using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        protected IDatabaseContext context;

        public HospitalRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public Hospital Add(Hospital hospital)
        {

            context.Hospitals.Add(hospital);
            return hospital;
        }

        public Hospital Delete(Guid id)
        {
            var entity = context.Hospitals.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Hospital with id = {id}");
            }
            context.Hospitals.Remove(entity);
            return entity;
        }

        public IEnumerable<Hospital> Find(Expression<Func<Hospital, bool>> predicate)
        {
            return context.Hospitals
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Hospital> GetAll()
        {
            return context.Hospitals.ToList();
        }

        public virtual Hospital? GetById(Guid id)
        {
            return context.Hospitals.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Hospital Update(Hospital entity)
        {
            context.Hospitals.Update(entity);
            return entity;
        }
    }
}
