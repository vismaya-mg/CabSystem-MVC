namespace CabSystem.Models
{

    public class Book
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }

        public string DriverName { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(User))]
        public String UserId { get; set; }

        public String Payment { get; set; }

        public string Status { get; set; }

    }
}
