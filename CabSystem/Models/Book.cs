namespace CabSystem.Models
{

    public class Book
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }

        public int Uid { get; set; }
        public ApplicationUser Driver { get; set; }

        [ForeignKey(nameof(Driver))]
        public String DriverId { get; set; }

    }
}
