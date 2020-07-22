using System.ComponentModel.DataAnnotations;

namespace Demo.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password 4 and 8 characters")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public bool CanUpdate { get; set; }
    }
}