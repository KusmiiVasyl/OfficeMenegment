using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeMenegment.Data;
using OfficeMenegment.Models;
using OfficeMenegment.Models.Department;

namespace OfficeMenegment.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly OfficeMenegmentDbContext _officeMenegmentDbContext;

        public DepartmentsController(OfficeMenegmentDbContext officeMenegmentDbContext)
        {
            _officeMenegmentDbContext = officeMenegmentDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _officeMenegmentDbContext.Departments.Include("Employees").ToListAsync();
            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDepartmentViewModel addDepartment)
        {
            var department = new DepartmentDbModel
            {
                Id = 0,
                Name = addDepartment.Name,
                Employees = addDepartment.Employees
            };

            await _officeMenegmentDbContext.Departments.AddAsync(department);
            await _officeMenegmentDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetNames()
        {
            var departments = await _officeMenegmentDbContext.Departments.ToListAsync();
            return View(departments);
        }

        [HttpPost]
        public async Task<IActionResult> GetNames(List<string> selectedDepartments, int id)
        {
            // Remove relationship with employee and departments
            var employees = await _officeMenegmentDbContext.Employees.Include("Departments").ToListAsync();
            foreach (var item in employees)
            {
                if (item.Id == id)
                {
                    item.Departments.Clear();
                }
            }
            await _officeMenegmentDbContext.SaveChangesAsync();

            // Add checked relationship with employee and departments
            var employee = await _officeMenegmentDbContext.Employees.FindAsync(id);
            var departments = await _officeMenegmentDbContext.Departments
                .Where(d => selectedDepartments.Contains(d.Name))
                .ToListAsync();

            if (departments != null && employee != null)
            {
                employee.Departments = new List<DepartmentDbModel>();
                foreach (var department in departments)
                {
                    employee.Departments.Add(department);
                }
            }
            await _officeMenegmentDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Employees");
        }

    }
}
