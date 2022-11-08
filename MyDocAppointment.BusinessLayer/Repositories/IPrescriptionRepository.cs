using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IPrescriptionRepository
    {
        void Add(Prescription prescription);
        void Delete(int id);
        IEnumerable<Prescription> GetAll();
        Prescription? GetById(int id);
        void Update(Prescription prescription);
        void AddMedication(int id, Medication medication);

    }
}
