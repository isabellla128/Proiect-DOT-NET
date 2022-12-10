using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocDoctor.BusinessLayer.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        protected IDatabaseContext context;

        public DoctorRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public Doctor Add(Doctor doctor)
        {

            context.Doctors.Add(doctor);
            return doctor;
        }

        public Doctor Delete(Guid id)
        {
            var entity = context.Doctors.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Doctor with id = {id}");
            }
            context.Doctors.Remove(entity);
            return entity;
        }

        public IEnumerable<Doctor> Find(Expression<Func<Doctor, bool>> predicate)
        {
            return context.Doctors
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Doctor> GetAll()
        {
            return context.Doctors.ToList();
        }

        public virtual Doctor? GetById(Guid id)
        {
            return context.Doctors.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Doctor Update(Doctor entity)
        {
            context.Doctors.Update(entity);
            return entity;
        }
    }
}
