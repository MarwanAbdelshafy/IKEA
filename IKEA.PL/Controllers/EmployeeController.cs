using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServise;
using IKEA.BLL.Services.EmployeeServise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {

        #region Servicse-DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices,IDepartementServices departementServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion
        #region Index


        [HttpGet]   // employee/index/
        public async Task<IActionResult> Index(string Search)
        {

            var Employees =await employeeServices.GetAllEmployees(Search);
            return View(Employees);

        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Department"]=departementServices.GetAllDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }

            try
            {
                var result =await employeeServices.CreateEmployee(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error: Could not create department.");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the department.");
                return View(employeeDto);
            }



        }
        #endregion


        #region Details
        [HttpGet]

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee =await employeeServices.GetEmployeeById(Id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);

        }


        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee = await employeeServices.GetEmployeeById(Id.Value);

            if (employee is null)
                return NotFound();

            var Mappedemployee = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Address = employee.Address,
                HiringDate = employee.HiringDate,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                ImasgeName=employee.ImageName,
                 

            };
            //ViewData["Department"] = departementServices.GetAllDepartments();

            return View(Mappedemployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);
            var Message = string.Empty;
            try
            {
                var Result =await employeeServices.UpdateEmployee(employeeDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Employee is Not Updated";
            }
            catch (Exception ex)
            {

                //1.log Exceptions
                logger.LogError(ex, Message);


                // 2. Set Message
                Message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Update the Employee";

            }
            //ViewData["Department"] = departementServices.GetAllDepartments();

            ModelState.AddModelError(string.Empty, Message);
            return View(employeeDto);
        }

        #endregion


        #region Delete
        [HttpGet]

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee =await employeeServices.GetEmployeeById(Id.Value);


            if (employee is null)
                return NotFound();


            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int EmpId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted =await employeeServices.DeleteEmployee(EmpId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                message = "Employee Is Not Deleted";

            }
            catch (Exception ex)
            {
                //1.log Exceptions
                logger.LogError(ex, message);


                // 2. Set Message
                message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Delete the Employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { Id = EmpId });
        }


        #endregion

    }
}
