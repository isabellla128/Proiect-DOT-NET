namespace MyDocAppointment.BusinessLayer.Entities
{
    public class Medication
    {
        public Medication(string name, int stock = 0)
        {
            Name = name;
            Stock = stock;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

    }
}
