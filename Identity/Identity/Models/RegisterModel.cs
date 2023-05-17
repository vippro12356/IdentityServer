using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
