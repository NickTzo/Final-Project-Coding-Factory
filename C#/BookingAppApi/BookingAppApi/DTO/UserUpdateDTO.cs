using System.ComponentModel.DataAnnotations;

namespace BookingAppApi.DTO
{
    public class UserUpdateDTO : BaseDTO
    {
        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Username { get; set; }

        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Firstname { get; set; }

        [StringLength(50, ErrorMessage = "The {0} field must be maximum of {characters}")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Spaces are not allowed")]
        public string? Lastname { get; set; }

        [StringLength(50, ErrorMessage = "E-mail should not exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid E-mail")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
