using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        protected IDatabaseContext context;

        public PatientRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public Patient Add(Patient patient)
        {

            context.Patients.Add(patient);
            return patient;
        }

        public Patient Delete(Guid id)
        {
            var entity = context.Patients.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Patient with id = {id}");
            }
            context.Patients.Remove(entity);
            return entity;
        }

        public IEnumerable<Patient> Find(Expression<Func<Patient, bool>> predicate)
        {
            return context.Patients
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Patient> GetAll()
        {
            return context.Patients.ToList();
        }

        public virtual Patient? GetById(Guid id)
        {
            return context.Patients.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Patient Update(Patient entity)
        {
            context.Patients.Update(entity);
            return entity;
        }
    }
}
