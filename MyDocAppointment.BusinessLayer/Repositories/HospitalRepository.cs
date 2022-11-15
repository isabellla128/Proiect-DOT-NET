using MyDocAppointment.BusinessLayer.Data;
using MyDocAppointment.BusinessLayer.Entities;
using MyDocAppointment.BusinessLayer.Repositories.Interfaces;

namespace MyDocAppointment.BusinessLayer.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly MyDocAppointmentDatabaseContext context;
        public HospitalRepository(MyDocAppointmentDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Hospital hospital)
        {
            context.Hospitals.Add(hospital);
            context.SaveChanges();
        }

        public void Update(Hospital hospital)
        {
            context.Update(hospital);
            context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var hospital = this.context.Hospitals.FirstOrDefault(d => d.Id == id);

            if (hospital == null)
            {
                throw new ArgumentException($"There is no hospital with given id: {id}");
            }
            context.Hospitals.Remove(hospital);
            context.SaveChanges();
        }
        public Hospital? GetById(Guid id)
        {
            return context.Hospitals.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Hospital> GetAll()
        {
            return context.Hospitals.ToList();
        }
    }
}
