using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class MedicationDosageHistoryRepository : IMedicationDosageHistoryRepository
    {
        protected IDatabaseContext context;

        public MedicationDosageHistoryRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public MedicationDosageHistory Add(MedicationDosageHistory medicationDosageHistory)
        {

            context.MedicationDosageHistories.Add(medicationDosageHistory);
            return medicationDosageHistory;
        }

        public MedicationDosageHistory Delete(Guid id)
        {
            var entity = context.MedicationDosageHistories.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no MedicationDosageHistory with id = {id}");
            }
            context.MedicationDosageHistories.Remove(entity);
            return entity;
        }

        public IEnumerable<MedicationDosageHistory> Find(Expression<Func<MedicationDosageHistory, bool>> predicate)
        {
            return context.MedicationDosageHistories
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual IEnumerable<MedicationDosageHistory> GetAll()
        {
            return context.MedicationDosageHistories.ToList();
        }

        public virtual MedicationDosageHistory? GetById(Guid id)
        {
            return context.MedicationDosageHistories.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual MedicationDosageHistory Update(MedicationDosageHistory entity)
        {
            context.MedicationDosageHistories.Update(entity);
            return entity;
        }
    }
}
