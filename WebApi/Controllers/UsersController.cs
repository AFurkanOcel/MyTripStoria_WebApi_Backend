using Contracts.UserDtos;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"The user with ID {id} was not found.");
            return Ok(user);
        }

        [HttpGet("by-username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound($"The user with Username:'{username}' was not found.");
            return Ok(user);
        }

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound($"The user with Email:'{email}' was not found.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPostDto userPostDto)
        {
            // DTO -> Entity
            var user = new User
            {
                Username = userPostDto.Username,
                Password = userPostDto.Password,
                Email = userPostDto.Email,
                PhoneNumber = userPostDto.PhoneNumber,
                Age = userPostDto.Age,
                CountryId = userPostDto.CountryId,
                CityId = userPostDto.CityId,
                Address = userPostDto.Address,
                Budget = userPostDto.Budget,
                IsPremium = userPostDto.IsPremium
            };

            var userForUsernameControl = await _userService.GetUserByUsernameAsync(userPostDto.Username);
            if (userForUsernameControl != null)
                return Conflict($"A user with the same Username:'{userPostDto.Username}' already exists.");

            var userForEmailControl = await _userService.GetUserByEmailAsync(userPostDto.Email);
            if (userForEmailControl != null)
                return Conflict($"A user with the same Email:'{userPostDto.Email}' already exists.");

            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserID }, userPostDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserPutDto userPutDto)
        {
            var userForControl = await _userService.GetUserByIdAsync(id);
            if (userForControl == null)
                return NotFound($"The user with ID {id} was not found.");

            // DTO -> Entity
            var user = new User
            {
                UserID = id,
                Username = userPutDto.Username,
                Password = userPutDto.Password,
                Email = userPutDto.Email,
                PhoneNumber = userPutDto.PhoneNumber,
                Age = userPutDto.Age,
                CountryId = userPutDto.CountryId,
                CityId = userPutDto.CityId,
                Address = userPutDto.Address,
                Budget = userPutDto.Budget,
                IsPremium = userPutDto.IsPremium
            };

            var userForUsernameControl = await _userService.GetUserByUsernameAsync(userPutDto.Username);
            if (userForUsernameControl != null)
                return Conflict($"A user with the same Username:'{userPutDto.Username}' already exists.");

            var userForEmailControl = await _userService.GetUserByEmailAsync(userPutDto.Email);
            if (userForEmailControl != null)
                return Conflict($"A user with the same Email:'{userPutDto.Email}' already exists.");

            var updatedUser = await _userService.UpdateUserAsync(user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"The user with ID {id} was not found.");

            await _userService.DeleteUserAsync(id);
            return Ok($"The user {user.Username} (ID:{id}) has been deleted.");
        }
    }
}
