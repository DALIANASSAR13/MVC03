
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.ViewModels;

namespace StudentManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Show all departments
        public async Task<IActionResult> ShowAll()
        {
            var departments = await _context.Departments
                .Include(d => d.Students)
                .ToListAsync();

            return View(departments);
        }

        // 2. Show one department and its ViewModel
        public async Task<IActionResult> ShowDetails(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Students)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            var viewModel = new DepartmentDetailsViewModel
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,
                DepartmentState = department.Students.Count > 50
                    ? "Main"
                    : "Branch",
                StudentsOver25 = department.Students
                    .Where(s => s.Age > 25)
                    .Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        // 3. Add: display the form
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Add: receive and save the form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowAll));
        }
    }
}
