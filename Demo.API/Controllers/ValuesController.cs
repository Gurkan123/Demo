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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostValue(ValueForPostDto valueForPostDto)
        {
            
            /*var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var permId = _context.Users.Find(userId).RoleId;


            if (_context.Perms.Find(permId).CanValuePost != true)
                return Unauthorized();*/
            

            var valueToCreate = _mapper.Map<Value>(valueForPostDto);

            _context.Values.Add(valueToCreate);

            await _context.SaveChangesAsync();

            return Ok(valueToCreate);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            // if ("user" == User.FindFirst(ClaimTypes.Role).Value)
            //  return Unauthorized();

           /* var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var permId = _context.Users.Find(userId).RoleId;


            if (_context.Perms.Find(permId).CanValueDelete != true)
                return Unauthorized();*/


            var value = await _context.Values.FindAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();

            return Ok(value);
        }
    }
}