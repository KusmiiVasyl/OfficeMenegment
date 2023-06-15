using Microsoft.AspNetCore.Mvc;
using OfficeMenegment.Models;
using OfficeMenegment.Data;
using Microsoft.EntityFrameworkCore;

namespace OfficeMenegment.Controllers;

public class EmployeesController : Controller
{
    private readonly OfficeMenegmentDbContext _officeMenegmentDbContext;

    public EmployeesController(OfficeMenegmentDbContext officeMenegmentDbContext)
    {
        _officeMenegmentDbContext = officeMenegmentDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employees = await _officeMenegmentDbContext.Employees.Include("Departments").ToListAsync();
        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
    {
        var employee = new EmployeeDbModel
        {
            Id = 0,
            FirstName = addEmployeeRequest.FirstName,
            LastName = addEmployeeRequest.LastName,
            Salary = addEmployeeRequest.Salary,
            Departments = addEmployeeRequest.Departments,
            DateBirthday = addEmployeeRequest.DateBirthday
        };

        await _officeMenegmentDbContext.Employees.AddAsync(employee);
        await _officeMenegmentDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var employee = await _officeMenegmentDbContext.Employees
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee != null)
        {
            var viewEmployee = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                Departments = employee.Departments,
                DateBirthday = employee.DateBirthday
            };
            return await Task.Run(() => View("Update", viewEmployee));
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateEmployeeViewModel updateEmployeeView)
    {
        var employee = await _officeMenegmentDbContext.Employees.FindAsync(updateEmployeeView.Id);

        if (employee != null)
        {
            employee.FirstName = updateEmployeeView.FirstName;
            employee.LastName = updateEmployeeView.LastName;
            employee.Salary = updateEmployeeView.Salary;
            employee.Departments = updateEmployeeView.Departments;
            employee.DateBirthday = updateEmployeeView.DateBirthday;

            await _officeMenegmentDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return RedirectToAction("Index"); //TODO create redirect when not update
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _officeMenegmentDbContext.Employees.FindAsync(id);

        if (employee != null)
        {
            _officeMenegmentDbContext.Remove(employee);
            await _officeMenegmentDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index"); //TODO Redirect when do not delete
    }
}
