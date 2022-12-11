﻿using System.Linq.Expressions;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task<T?> Delete(Guid id);
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<T> Update(T entity);
        Task<IReadOnlyCollection<T>> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}