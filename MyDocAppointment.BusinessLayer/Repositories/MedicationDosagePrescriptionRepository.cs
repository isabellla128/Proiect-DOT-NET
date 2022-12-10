using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationDosagePrescriptionRepository : IMedicationDosagePrescriptionRepository
    {
        protected IDatabaseContext context;

        public MedicationDosagePrescriptionRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public MedicationDosagePrescription Add(MedicationDosagePrescription MedicationDosagePrescription)
        {

            context.MedicationDosagePrescriptions.Add(MedicationDosagePrescription);
            return MedicationDosagePrescription;
        }

        public MedicationDosagePrescription Delete(Guid id)
        {
            var entity = context.MedicationDosagePrescriptions.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no MedicationDosagePrescription with id = {id}");
            }
            context.MedicationDosagePrescriptions.Remove(entity);
            return entity;
        }

        public IEnumerable<MedicationDosagePrescription> Find(Expression<Func<MedicationDosagePrescription, bool>> predicate)
        {
            return context.MedicationDosagePrescriptions
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<MedicationDosagePrescription> GetAll()
        {
            return context.MedicationDosagePrescriptions.ToList();
        }

        public virtual MedicationDosagePrescription? GetById(Guid id)
        {
            return context.MedicationDosagePrescriptions.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual MedicationDosagePrescription Update(MedicationDosagePrescription entity)
        {
            context.MedicationDosagePrescriptions.Update(entity);
            return entity;
        }
    }
}
