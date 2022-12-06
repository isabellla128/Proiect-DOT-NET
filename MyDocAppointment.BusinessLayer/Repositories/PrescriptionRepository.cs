using Microsoft.EntityFrameworkCore;
using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        protected IDatabaseContext context;

        public PrescriptionRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public Prescription Add(Prescription Prescription)
        {

            context.Prescriptions.Add(Prescription);
            return Prescription;
        }

        public Prescription Delete(Guid id)
        {
            var entity = context.Prescriptions.Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no Prescription with id = {id}");
            }
            context.Prescriptions.Remove(entity);
            return entity;
        }

        public IEnumerable<Prescription> Find(Expression<Func<Prescription, bool>> predicate)
        {
            return context.Prescriptions
                .AsQueryable()
                .Where(predicate).ToList();
        }


        public virtual Prescription? GetById(Guid id)
        {
            return context.Prescriptions.Find(id);
        }

        public void SaveChanges()
        {
            context.Save();
        }

        public virtual Prescription Update(Prescription entity)
        {
            context.Prescriptions.Update(entity);
            return entity;
        }

        public IEnumerable<Prescription> GetAll()
        {
            return context.Prescriptions.Include(p => p.MedicationDosagePrescriptions).ToList();

        }
    }
}
