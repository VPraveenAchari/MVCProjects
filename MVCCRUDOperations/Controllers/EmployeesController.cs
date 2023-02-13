using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCRUDOperations.Data;
using MVCCRUDOperations.Models;
using MVCCRUDOperations.Models.Domain;

namespace MVCCRUDOperations.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.EmployeeDetails.ToListAsync();
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
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth

            };
            await mvcDemoDbContext.EmployeeDetails.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
        /* [HttpPost]
         public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
         {
             var employee = new Employee()
             {
                 Id = Guid.NewGuid(),
                 Name = addEmployeeRequest.Name,
                 Email = addEmployeeRequest.Email,
                 Salary = addEmployeeRequest.Salary,
                 Department = addEmployeeRequest.Department,
                 DateOfBirth = addEmployeeRequest.DateOfBirth

             };
              mvcDemoDbContext.EmployeeDetails.Add(employee);
              mvcDemoDbContext.SaveChanges();
             return RedirectToAction("Add");
         }*/
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth

                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.EmployeeDetails.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee=await mvcDemoDbContext.EmployeeDetails.FindAsync(model.Id);
            if(employee != null)
            {
                mvcDemoDbContext.EmployeeDetails.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
