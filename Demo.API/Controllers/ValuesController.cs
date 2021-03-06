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
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ValuesController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostValue(ValueForPostDto valueForPostDto)
        {
            
            var yetki = AuthorizeControl("ValuePost");

            if (yetki == false)
                return Unauthorized();
            

            var valueToCreate = _mapper.Map<Value>(valueForPostDto);

            _context.Values.Add(valueToCreate);

            await _context.SaveChangesAsync();

            return Ok(valueToCreate);

        }

        // DELETE api/values/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var yetki = AuthorizeControl("ValueDelete");

            if (yetki == false)
                return Unauthorized();


            var value = await _context.Values.FindAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();

            return Ok(value);
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