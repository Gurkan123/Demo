using System.Collections.Generic;

namespace Demo.API.Models
{
    public class Perm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RolePerm> RolePerms { get; set; }
    }
}