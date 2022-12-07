namespace CabSystem.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public int Cost { get; set; }


    }
}
