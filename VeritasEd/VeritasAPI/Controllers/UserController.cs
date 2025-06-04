using Microsoft.AspNetCore.Mvc;
using VeritasEd.Api.Data;
using VeritasEd.Api.Models;

namespace VeritasEd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context) => _context = context;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest("Username already exists.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == login.Username && u.Password == login.Password);

            if (user == null)
                return Unauthorized();

            return Ok(new { user.Id, user.Username, user.Role });
        }
        [HttpGet("students")]
        public IActionResult GetStudents()
        {
            var students = _context.Users.Where(u => u.Role == "Student").ToList();
            return Ok(students);
        }
    }
}