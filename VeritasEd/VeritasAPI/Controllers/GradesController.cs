using Microsoft.AspNetCore.Mvc;
using VeritasEd.Api.Data;
using VeritasEd.Api.Models;

namespace VeritasEd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GradesController(AppDbContext context) => _context = context;

        [HttpGet("{userId}")]
        public IActionResult GetGrades(int userId)
        {
            var grades = _context.Grades.Where(g => g.UserId == userId).ToList();
            return Ok(grades);
        }
        [HttpGet]
        public IActionResult GetAllGrades()
        {
            var grades = _context.Grades.ToList();
            return Ok(grades);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return NotFound();
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] Grade updatedGrade)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return NotFound();

            grade.Subject = updatedGrade.Subject;
            grade.Value = updatedGrade.Value;
            grade.UserId = updatedGrade.UserId;

            await _context.SaveChangesAsync();
            return Ok(grade);
        }
    }
}