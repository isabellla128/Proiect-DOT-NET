namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Medication
    {
        public Medication()
        {

        }
        public Medication(string name, int stock = 0)
        {
            Name = name;
            Stock = stock;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Stock { get; set; }

    }
}
