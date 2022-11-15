using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(Guid id);
        IEnumerable<Doctor> GetAll();
        T? GetById(Guid id);
        void Update(T entity);
    }
}
