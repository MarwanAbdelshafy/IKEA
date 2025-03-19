using System.CodeDom;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Plugins;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartementServices departementServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartementServices _departementService, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departementServices = _departementService;
            logger = _logger;
            this.environment = environment;
        }
        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departementServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion


        #region Details
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var department = departementServices.GetDepartmentById(Id.Value);
            if (department is null)
                return NotFound();
            return View(department);

        }


        #endregion


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var Message = string.Empty;
            try
            {
                var Result = departementServices.CreateDepartment(departmentDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    Message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }
                else
                {
                    Message = "An Error Effect at The Creation Operator";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }
            }



        }

        #endregion


        #region Update
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var Department = departementServices.GetDepartmentById(Id.Value);

            if (Department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate,

            };

            return View(MappedDepartment);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if(!ModelState.IsValid)
                return View(departmentDto) ;
            var Message = string.Empty;
            try
            {
                var Result = departementServices.UpdateDepartment(departmentDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is Not Updated";
            }
            catch (Exception ex)
            {

                //1.log Exceptions
                logger.LogError(ex, Message);


                // 2. Set Message
                Message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Update the Department";

            }

            ModelState.AddModelError(string.Empty, Message);
            return View(departmentDto);
        }

        #endregion


        #region Delete
        [HttpGet]

        public IActionResult Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var Department = departementServices.GetDepartmentById(Id.Value);

            if (Department is null)
                return NotFound();


            return View(Department);
        }

        [HttpPost]
        public IActionResult Delete (int DeptId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted=departementServices.DeleteDepartment(DeptId);
                if (IsDeleted) 
                    return RedirectToAction(nameof(Index));

                message = "Department Is Not Deleted";
                    
            } 
            catch (Exception ex)
            {
                //1.log Exceptions
                logger.LogError(ex, message);


                // 2. Set Message
                message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Delete the Department";

            }
            ModelState.AddModelError(string.Empty,message);
            return RedirectToAction(nameof(Delete),new {Id= DeptId });
        }


        #endregion
    }

}
