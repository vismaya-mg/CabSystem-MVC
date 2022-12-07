namespace CabSystem.Models
{
    public enum VehicleType
    {
        Car,
        Bike,
        Traveller,
    }
    
    public class Cab
    {
        public int Id { get; set; }

        public VehicleType Vehicle { get; set; }
        [StringLength(70)]

        public string VechicleNumber { get; set; }
        [StringLength(70)]

        public string Model { get; set; }

        public string? Description { get; set; }

        public ApplicationUser User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }


    }
}
