using ExoBlazor_API.Models.DTOs;
using ExoBlazor_API.Repositories;
using ExoBlazor_API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace ExoBlazor_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtGenerator _jwtGenerator;

        public UserController(UserRepository userRepository, JwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegisterDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            if (_userRepository.Register(user.Login, hashedPassword, user.Email))
            {
                return Ok("Registration successful");
            }

            return BadRequest("A problem occurred during registration.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string hashedPassword = _userRepository.GetPassword(user.Login);

            if (BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword))
            {
                string token = _jwtGenerator.GenerateToken(_userRepository.Login(user.Login, hashedPassword));

                return Ok(token);
            }

            return BadRequest("Invalid password");
        }
    }
}
