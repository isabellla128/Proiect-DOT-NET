using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationRepositrory : IMedicationRepositrory
    {
        protected IDatabaseContext context;

        public MedicationRepositrory(IDatabaseContext context)
        {
            this.context = context;
        }

        public Medication Add(Medication medication)
        {

            context.Medications.Add(medication);
            return medication;
        }

        public Medication Delete(Guid id)
        {
            var entity = context.Medications.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Medication with id = {id}");
            }
            context.Medications.Remove(entity);
            return entity;
        }

        public IEnumerable<Medication> Find(Expression<Func<Medication, bool>> predicate)
        {
            return context.Medications
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<Medication> GetAll()
        {
            return context.Medications.ToList();
        }

        public virtual Medication? GetById(Guid id)
        {
            return context.Medications.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Medication Update(Medication entity)
        {
            context.Medications.Update(entity);
            return entity;
        }
    }
}
