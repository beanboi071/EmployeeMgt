using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
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
        public IActionResult Detail(int? id)
        {
            HomeDetailViewModel homeDetailViewModel = new HomeDetailViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                pgTitle = "Detail page"
            };
            if(homeDetailViewModel.Employee == null)
            {
                return RedirectToAction("Error/NotFound");
            }
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

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee emp = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel model = new EmployeeEditViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Email = emp.Email,
                Department = emp.Department,
                ExistingPhotoPath = emp.Photo
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employee)
        {
            string? uniqueFileName = null;
            if (employee.PhotoPath != null)
            {
                
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.PhotoPath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    employee.PhotoPath.CopyTo(fileStream);
                }
                if (employee.ExistingPhotoPath != null)
                {
                    System.IO.File.Delete(Path.Combine(uploadsFolder, employee.ExistingPhotoPath));
                }
            }


            Employee emp = new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                Photo = uniqueFileName
            };
            _employeeRepository.Update(emp);
            return RedirectToAction("Detail", new { id = employee.Id });
        }

        public IActionResult Privacy()
        { 
            return View();
        }
        [HttpDelete]
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