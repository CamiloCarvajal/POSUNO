using System.ComponentModel.DataAnnotations;

namespace POSUNO.Api.Models
{
    public class LoginRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
