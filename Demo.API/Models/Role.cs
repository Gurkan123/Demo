using System.Collections.Generic;

namespace Demo.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RolePerm> RolePerms { get; set; }
        
    }
}