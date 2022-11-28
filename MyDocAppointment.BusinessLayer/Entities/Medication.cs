namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Medication
    {
        public Medication(string name, int stock = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Stock = stock;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Stock { get; set; }
    }
}
