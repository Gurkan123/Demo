using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Demo.API.Data;
using Demo.API.Dtos;
using Demo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PermController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> GetPerms()
        {
            var perms = await _context.Perms.ToListAsync();
            return Ok(perms);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostValue(PermForPostDto permForPostDto)
        {
            var yetki = AuthorizeControl("PermPost");

            if (yetki == false)
                return Unauthorized();

            var permToCreate = _mapper.Map<Perm>(permForPostDto);

            _context.Perms.Add(permToCreate);

            await _context.SaveChangesAsync();

            return Ok(permToCreate);

        }


        // DELETE api/values/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerm(int id)
        {
            var yetki = AuthorizeControl("PermDelete");

            if (yetki == false)
                return Unauthorized();

            var perm = await _context.Perms.FindAsync(id);

            if (perm == null)
            {
                return NotFound();
            }

            _context.Perms.Remove(perm);
            await _context.SaveChangesAsync();

            return Ok(perm);
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