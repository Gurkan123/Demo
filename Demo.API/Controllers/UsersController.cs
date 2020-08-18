using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Demo.API.Data;
using Demo.API.Dtos;
using Demo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IMapper mapper, IAuthRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
           /* if ("True" != (User.FindFirst("canUpdate").Value))
            {
                if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    return Unauthorized("You are not alllowed to do it.");
            }*/

            userForUpdateDto.Role = userForUpdateDto.Role.ToLower();
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);
            var userToReturn = _mapper.Map<UserToReturnDto>(userFromRepo);

            if (await _repo.SaveAll())
                return Ok(userToReturn);

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersFromRepo = await _repo.GetUsers();

            return Ok(usersFromRepo);
        }
    }
}