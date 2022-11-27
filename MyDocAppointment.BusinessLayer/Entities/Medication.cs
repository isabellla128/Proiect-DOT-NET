namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Medication
    {
        public Medication(string name, int stock = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Stock = stock;
            Histories = new List<History>();
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Stock { get; set; }
    
        public ICollection<History> Histories { get; private set; }
    }
}
