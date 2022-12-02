namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Medication
    {
        public Medication(string name, int stock, string unit, int capacity, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Stock = stock;
            //ramane de vazut daca pastram prescriptions si histories
            Prescriptions = new List<Prescription>();
            Histories = new List<History>();
            Unit = unit;
            Capacity = capacity;
            Price = price;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Stock { get; set; }

        public string Unit { get; private set; }
        public int Capacity { get; private set; }
        public double Price { get; private set; }

        public ICollection<Prescription> Prescriptions { get; private set; }
    
        public ICollection<History> Histories { get; private set; }
    }
}
