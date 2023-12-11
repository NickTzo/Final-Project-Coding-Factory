using System.ComponentModel.DataAnnotations;

namespace BookingAppApi.DTO
{
    public class CarCreateDTO
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Model { get; set; }

        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Year { get; set; }

        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Seat { get; set; }

        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Cc { get; set; }

        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Transmission { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public double Price { get; set; }
        public int? UserId { get; set; }
        public bool? IsVisible { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
