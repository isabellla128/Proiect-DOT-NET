using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Doctor Add(Doctor doctor);
        Doctor Delete(Guid id);
        IEnumerable<Doctor> Find(Expression<Func<Doctor, bool>> predicate);
        IEnumerable<Doctor> GetAll();
        Doctor? GetById(Guid id);
        void SaveChanges();
        Doctor Update(Doctor entity);
    }
}