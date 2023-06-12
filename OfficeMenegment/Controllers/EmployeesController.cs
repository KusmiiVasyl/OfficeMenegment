using Microsoft.AspNetCore.Mvc;

namespace OfficeMenegment.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
