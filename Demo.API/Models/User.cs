using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public Role Roles { get; set; }
        public int RoleId { get; set; }
        
    }
}