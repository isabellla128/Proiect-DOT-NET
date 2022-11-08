using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories.Interfaces
{
    public interface IHospitalRepository
    {
        void Add(Hospital hospital);
        void Delete(int id);
        IEnumerable<Hospital> GetAll();
        Hospital? GetById(int id);
        void Update(Hospital hospital);
    }
}
