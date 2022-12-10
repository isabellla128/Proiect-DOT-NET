using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IPrescriptionRepository
    {
        Prescription Add(Prescription Prescription);
        Prescription Delete(Guid id);
        IEnumerable<Prescription> Find(Expression<Func<Prescription, bool>> predicate);
        IEnumerable<Prescription> GetAll();
        Prescription? GetById(Guid id);
        void SaveChanges();
        Prescription Update(Prescription entity);
    }
}