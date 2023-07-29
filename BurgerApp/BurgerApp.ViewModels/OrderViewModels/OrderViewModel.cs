using System.ComponentModel.DataAnnotations;

namespace BurgerApp.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Customer Full Name must be at least 5 characters long.")]
        [RegularExpression(@"^[A-Z][A-Za-z0-9 .,!@#$%^&*()_+=\-\\[\]{}|;:'""<>,.?/~`]{4,}$", ErrorMessage = "Customer Full Name must start with a capital letter.")]
        [Display(Name = "Customer Full Name")]
        public string FullName { get; set; } = string.Empty;
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Customer Address must be at least 5 characters long.")]
        [Display(Name = "Customer Address")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "Shop Name")]
        public string LocationName { get; set; } = string.Empty;
        [Display(Name = "Shop Address")]
        public string LocationAddress { get; set; } = string.Empty;
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }
        [Display(Name = "Burgers")]
        public List<string>? BurgerNames { get; set; }
        public int LocationId { get; set; }
    }
}