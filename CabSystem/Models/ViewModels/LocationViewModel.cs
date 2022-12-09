namespace CabSystem.Models.ViewModels
{
    public class LocationViewModel
    {
        [Required]
        [Display(Name = "From")]
        public string From { get; set; }
        [Required]
        [Display(Name = "To")]
        public string To { get; set; }


    }
}
