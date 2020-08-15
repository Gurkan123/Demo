using System.Collections.Generic;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.API.Dtos
{
    public class RoleCreateDto
    {
        public string Name { get; set; }
        public List<Perm> Perms { get; set; }
    }
}