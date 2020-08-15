namespace Demo.API.Models
{
    public class RolePerm
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermId { get; set; }
        public Perm Perm { get; set; }

    }
}