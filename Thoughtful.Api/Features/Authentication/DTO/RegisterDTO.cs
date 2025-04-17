using System.ComponentModel.DataAnnotations;

namespace Thoughtful.Api.Features.Authentication.DTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
