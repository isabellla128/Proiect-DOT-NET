using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IHospitalRepository
    {
        Hospital Add(Hospital hospital);
        Hospital Delete(Guid id);
        IEnumerable<Hospital> Find(Expression<Func<Hospital, bool>> predicate);
        IEnumerable<Hospital> GetAll();
        Hospital? GetById(Guid id);
        void SaveChanges();
        Hospital Update(Hospital entity);
    }
}