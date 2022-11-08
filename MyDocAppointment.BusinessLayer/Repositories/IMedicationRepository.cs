using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IMedicationRepository
    {
        void Add(Medication medication);
        void Delete(int id);
        IEnumerable<Medication> GetAll();
        Medication? GetById(int id);
        void Update(Medication medication);
    }
}
