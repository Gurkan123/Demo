using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Demo.API.Data;
using Demo.API.Dtos;
using Demo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RoleController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpPost("roleperm")]
        public async Task<IActionResult> PostRolePerm(RoleCreateDto roleCreateDto)
        {

            var roleName = _context.Roles.Where(u => u.Name == roleCreateDto.RoleName).ToList();
            
            var roleId = 0;

            foreach (var item in roleName)
            {
                roleId = item.Id;
            }

            RolePerm rolePerm = new RolePerm();
            
            foreach (var perm in roleCreateDto.Permissions)
            {
                rolePerm.RoleId = roleId;
                rolePerm.PermId = int.Parse(perm);
                _context.RolePerms.Add(rolePerm);
                await _context.SaveChangesAsync();
            };

            return Ok(rolePerm);
        }


        [HttpPost("role")]
        public async Task<IActionResult> PostRole(RoleCreateDto roleCreateDto)
        {
            //var yetki = AuthorizeControl("RoleCreate");

            //if (yetki == false)
                //return Unauthorized();

            Role role = new Role()
            {
                Name = roleCreateDto.RoleName
            };

            _context.Roles.Add(role);

            await _context.SaveChangesAsync();
            return Ok(role);

        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        public bool AuthorizeControl(string permission)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var roleId = _context.Users.Find(userId).RoleId;

            var roleperms = _context.RolePerms.Where(x => x.RoleId == roleId).ToList();


            var perms = _context.Perms.Where(x => x.Name == permission).ToList();

            var permId = 0;

            var yetki = false;

            foreach (var item in perms)
            {
                permId = item.Id;
            }
            
            foreach (var item in roleperms)
            {
                if(item.PermId == permId){
                    yetki = true;
                }
            }

            return yetki;
        }
        
    }
}