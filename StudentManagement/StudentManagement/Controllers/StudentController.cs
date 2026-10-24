
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;

namespace StudentManagement.Controllers
{
    public class StudentController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // /Student/ShowAll
        public async Task<IActionResult> ShowAll()
        {
            var students = await _context.Students
                .Include(s => s.Department)
                .ToListAsync();

            return View(students);
        }

        // /Student/ShowDetails?id=3
        public async Task<IActionResult> ShowDetails(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Results)
                    .ThenInclude(r => r.Course)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}
