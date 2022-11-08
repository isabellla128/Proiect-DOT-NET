using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IDoctorRepository
    {
        void Add(Doctor doctor);
        void Delete(int id);
        IEnumerable<Doctor> GetAll();
        Doctor? GetById(int id);
        void Update(Doctor doctor);
    }
}
