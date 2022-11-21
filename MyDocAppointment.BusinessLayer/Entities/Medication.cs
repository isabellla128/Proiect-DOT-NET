namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Medication
    {
        public Medication(string name, int stock = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Stock = stock;
            Prescriptions = new List<Prescription>();
            Historys = new List<History>();
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Stock { get; set; }

        public ICollection<Prescription> Prescriptions { get; private set; }

    
        public ICollection<History> Historys { get; private set; }
    }
}
