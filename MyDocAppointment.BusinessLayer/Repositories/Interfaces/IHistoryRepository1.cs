using MyDocAppointment.BusinessLayer.Entities;
using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IHistoryRepository1
    {
        History Add(History history);
        History Delete(Guid id);
        IEnumerable<History> Find(Expression<Func<History, bool>> predicate);
        IEnumerable<History> GetAll();
        History? GetById(Guid id);
        void SaveChanges();
        History Update(History entity);
    }
}