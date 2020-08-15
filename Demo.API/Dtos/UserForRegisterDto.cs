using System.ComponentModel.DataAnnotations;
using Demo.API.Models;

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

        public Role Roles { get; set; }
        public int RoleId { get; set; }
    }
}