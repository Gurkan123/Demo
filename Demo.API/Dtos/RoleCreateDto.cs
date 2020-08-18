using System.Collections.Generic;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.API.Dtos
{
    public class RoleCreateDto
    {
        public string RoleName { get; set; }
        public string[] Permissions { get; set; }
    }
}