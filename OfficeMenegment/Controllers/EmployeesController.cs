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
        var employees = await _officeMenegmentDbContext.Employees.ToListAsync();
        return View(employees);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
    {
        var employee = new EmployeeModel
        {
            Id = 0,
            FirstName = addEmployeeRequest.FirstName,
            LastName = addEmployeeRequest.LastName,
            Salary = addEmployeeRequest.Salary,
            Department = addEmployeeRequest.Department,
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

        if(employee != null)
        {
            var viewEmployee = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                Department = employee.Department,
                DateBirthday = employee.DateBirthday
            };
            return await Task.Run(()=> View("Update",viewEmployee));
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateEmployeeViewModel updateEmployeeView)
    {
        var employee = await _officeMenegmentDbContext.Employees.FindAsync(updateEmployeeView.Id);

        if(employee != null)
        {
            employee.FirstName = updateEmployeeView.FirstName;
            employee.LastName = updateEmployeeView.LastName;
            employee.Salary = updateEmployeeView.Salary;
            employee.Department = updateEmployeeView.Department;
            employee.DateBirthday = updateEmployeeView.DateBirthday;

            await _officeMenegmentDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return RedirectToAction("Index"); //TODO create redirect to not update
    }


}
