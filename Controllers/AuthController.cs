using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using OnlineBookstore.Repositories;
using OnlineBookstore.Services;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            var existingUser = await _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null)
                return BadRequest("Username is already taken.");

            using (var hmac = new HMACSHA512())
            {
                user.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash)));
                user.PasswordSalt = Convert.ToBase64String(hmac.Key); // Store the key (salt)
            }

            await _userRepository.AddUser(user);
            return Ok("User registered successfully.");
        }  

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User loginUser)
        {
            var user = await _userRepository.GetUserByUsername(loginUser.Username);
            if (user == null)
                return Unauthorized("Invalid username");

            using (var hmac = new HMACSHA512(Convert.FromBase64String(user.PasswordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.PasswordHash));
                if (Convert.ToBase64String(computedHash) != user.PasswordHash)
                    return Unauthorized("Invalid password.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(token);
        }

    }
}
