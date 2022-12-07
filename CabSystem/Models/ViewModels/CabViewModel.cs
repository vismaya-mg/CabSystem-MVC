namespace CabSystem.Models.ViewModels
{
    public class CabViewModel
    {
        [Required]
        [Display(Name = "Vehicle Type")]
        public VehicleType Vehicle { get; set; }
        [Required]
        [Display(Name = "Vechicle Number")]
        public string VechicleNumber { get; set; }
        [Required]
        [Display(Name = "Vechicle Model")]
        public string Model { get; set; }
        [Required]
        [Display(Name = "Vechicle Description")]
        public string? Description { get; set; }
    }
}
