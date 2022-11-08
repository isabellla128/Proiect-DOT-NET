using MyDocAppointment.BusinessLayer.Entities;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        void Delete(int id);
        IEnumerable<Appointment> GetAll();
        Appointment? GetById(int id);
        void Update(Appointment appointment);
    }
}
