using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostEnvironment _hostEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostEnvironment = webHostEnvironment;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }
        public ViewResult Detail(int? id)
        {
            HomeDetailViewModel homeDetailViewModel = new HomeDetailViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                pgTitle = "Detail page"
            };
            return View(homeDetailViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = null;
                if(model.PhotoPath != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath,"wwwroot/images");
                    uniqueFileName = Guid.NewGuid().ToString()+"_"+model.PhotoPath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.PhotoPath.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    Photo = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Detail", new { id = newEmployee.Id });
            }
            else
            {
                return View();
            }
        }


        public IActionResult Privacy()
        { 
            return View();
        }

        public IActionResult Delete(int id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}