using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> PostRole(RoleCreateDto roleCreateDto)
        {

            var roleToCreate = _mapper.Map<Role>(roleCreateDto);
            _context.Roles.Add(roleToCreate);
            await _context.SaveChangesAsync();
            return Ok(roleToCreate);

        }

        [HttpGet]
        public async Task<IActionResult> GetPerms()
        {
            var perms = await _context.Perms.ToListAsync();
            return Ok(perms);
        }
    }
}