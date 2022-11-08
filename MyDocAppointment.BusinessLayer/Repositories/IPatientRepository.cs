using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        void Delete(int id);
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
        void Update(Patient patient);
    }
}
