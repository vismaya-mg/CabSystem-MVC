namespace CabSystem.Models.ViewModels
{
    public class BookViewModel
    {
        [Required]

        [Display(Name = "PickUp Location:")]
        public IEnumerable<string>? PickupPoints { get; set; }
        public string Pickup { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 1)]
        [Display(Name = "Drop Down Location:")]
        public string Drop { get; set; }

        [Required]
        [Display(Name = "Date& Time:")]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public int Cost { get; set; }
        public string Payment { get; set; }

       
    }
}
