namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Delete(Guid id);
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        T Update(T entity);
    }
}
