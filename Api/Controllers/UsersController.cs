using AutoMapper;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;
using ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUser Repo;
        readonly IMapper Map;
        public UsersController(IUser repo, IMapper map)
        {
            Repo = repo;
            Map = map;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var result =  await Repo.Get();
            return Ok( Map.Map<List<UserDto>>(result));

        }

        

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post(UserDto  userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = Map.Map<User>(userDto);

            bool exist = await Repo.Exist(user.Email);

            if (!exist)
            {
                var result = await Repo.Create(user);

                return result ? Ok(result) : BadRequest(result);
            }
            return BadRequest( new {value = true, mjs = "ya existe el usuario " });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var login = Map.Map<Login>(loginDto);

            var result = await Repo.Login(login);

            return result != null ? Ok(new { value = true, obj=result }) : Ok(new { value = false });
        }

        // PUT api/<UsersController>/
        [HttpPut]
        public async Task<ActionResult<bool>> Put(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = Map.Map<User>(userDto);

            var result = await Repo.Update(user);

            return result ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> ChangePassword(ChangedPasswordDto changedPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var changedPasswor = Map.Map<ChangedPassword>(changedPasswordDto);

            var result = await Repo.ChangePassword(changedPasswor);

            return result ? Ok(result) : BadRequest(result);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await Repo.Delete(id);

            return result ? Ok(result) : BadRequest(result);
        }
    }
}
