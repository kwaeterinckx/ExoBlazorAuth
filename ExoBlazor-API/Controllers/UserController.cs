using ExoBlazor_API.Models.DTOs;
using ExoBlazor_API.Repositories;
using ExoBlazor_API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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

            if (_userRepository.Register(user.Login, user.Email, hashedPassword))
            {
                return Ok("Registration successful");
            }

            return BadRequest("An error occurred during registration.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? hashedPassword = _userRepository.GetPassword(user.Login);

            if (hashedPassword is not null && BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword))
            {
                string token = _jwtGenerator.GenerateToken(_userRepository.Login(user.Login, hashedPassword!)!);

                return Ok(token);
            }

            return BadRequest("Invalid password");
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetUsers());
        }

        [Authorize("UserRequired")]
        [HttpGet("Profile")]
        public IActionResult GetProfile()
        {
            string tokenFromRequest = HttpContext.Request.Headers["Authorization"];
            string token = tokenFromRequest.Substring(7, tokenFromRequest.Length - 7);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(token);
            string login = jwtSecurityToken.Claims.Single(l => l.Type == "Login").Value;

            return Ok(_userRepository.GetProfileByLogin(login));
        }
    }
}
