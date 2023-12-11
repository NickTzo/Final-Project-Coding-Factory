using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAppApi.Data;
using AutoMapper;
using BookingAppApi.DTO;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BookingAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IMapper _mapper;
        


        public UsersController(IMapper mapper,IUnitOfWorkService unitOfWorkService)
        {
            _mapper = mapper;
            _unitOfWorkService = unitOfWorkService;
        }


        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var usersDto =await  _unitOfWorkService.User.GetAll();
            return Ok(usersDto);
        }


        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUser(int id)
        {
            var user = await _unitOfWorkService.User.GetUserById(id);
            if (await _unitOfWorkService.User.GetAll() == null || user is null)
            {
                return NotFound();
            }
                return Ok(user);
        }


        // GET: api/Users/user
        [HttpGet("user/{username}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserByUsername(string username)
        {
            var user = await _unitOfWorkService.User.GetUserByUsername(username);
            if (await _unitOfWorkService.User.GetAll() == null || user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        // PUT: api/Users/5
        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> PutUser(UserUpdateDTO userUpdateDto)
        {
            UserReadOnlyDTO user = await _unitOfWorkService.User.GetUserById(userUpdateDto.Id);
            if (user == null ||await _unitOfWorkService.User.GetAll() == null)
            {
                return NotFound();
            }
            try
            {
               user = await _unitOfWorkService.User.UpdateUserAsync(userUpdateDto.Id,userUpdateDto);
                return Ok(user);
            }
            catch (Exception)
            {
                return Problem("Internal Server Error");
            }

        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserCreateDTO userCreateDto)
        {
            if (await _unitOfWorkService.User.GetAll() == null)
            {
                return Problem("Entity set 'Users'  is null.");
            }
            if (userCreateDto == null)
            {
                return BadRequest();
            }
            try
            {
                UserReadOnlyDTO dto = await _unitOfWorkService.User.SingUpUserAsync(userCreateDto);
                return CreatedAtAction(nameof(GetUser), new { id = dto.Id }, dto);
            }
            catch (Exception)
            {
                return Problem("Internal Server Error");
            } 
        }


        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _unitOfWorkService.User.GetUserById(id) == null || await _unitOfWorkService.User.GetAll() is null)
            {
                return NotFound();
            }
            try
            {
                await _unitOfWorkService.User.DeleteUserAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return Problem("Internal Server Error");
            }

        }

        // LOGIN: api/Users
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDto)
        {
           UserReadOnlyDTO user = await _unitOfWorkService.User.SingInUserAsync(userLoginDto);
            if(user == null) 
            {
                return Unauthorized();
            }

            UserReadOnlyDTO userLogin = _mapper.Map<UserReadOnlyDTO>(user);
            var userToken = _unitOfWorkService.User.CreateUserToken(user.Id, user.Username);
            userLogin.Token = userToken;

            return Ok(userLogin);
        }
    }
}
