 using System.CodeDom;
using System.Threading.Tasks;
using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServise;
using IKEA.DAL.Models.Department;
using IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Plugins;

namespace IKEA.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller

    {
        private readonly IDepartementServices departementServices;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartementServices _departementService,IMapper mapper ,ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departementServices = _departementService;
            this.mapper = mapper;
            logger = _logger;
            this.environment = environment;
        }
        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            var Departments =  departementServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var department =await departementServices.GetDepartmentById(Id.Value);
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
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var Message = string.Empty;
            try
            {
                var departmentDto= mapper.Map<DepartmentVM,CreatedDepartmentDto>(departmentVM); 

                //var departmentDto = new CreatedDepartmentDto()
                //    {
                //      Name = departmentVM.Name,
                //      Code = departmentVM.Code,
                //      CreationDate = departmentVM.CreationDate,
                //      Description = departmentVM.Description,
                //    };
                var Result =await departementServices.CreateDepartment(departmentDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is not Created";

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                    Message = ex.Message;
                else
                   Message = "An Error Effect at The Creation Operator";

            }

            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);

        }

        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var Department =await departementServices.GetDepartmentById(Id.Value);

            if (Department is null)
                return NotFound();

            var MappedDepartment = new DepartmentVM()
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
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(DepartmentVM departmentVM)
        {
            if(!ModelState.IsValid)
                return View(departmentVM) ;
            var Message = string.Empty;
            try
            {

                var departmentDto = mapper.Map<DepartmentVM, UpdatedDepartmentDto>(departmentVM);

                //var departmentDto=new UpdatedDepartmentDto()
                //{
                //    Id=departmentVM.Id,
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    CreationDate = departmentVM.CreationDate,
                //    Description = departmentVM.Description,
                //}; ;
                var Result =await departementServices.UpdateDepartment(departmentDto);
                
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
            return View(departmentVM);
        }

        #endregion


        #region Delete
        [HttpGet]

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var Department =await departementServices.GetDepartmentById(Id.Value);

            if (Department is null)
                return NotFound();


            return View(Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete (int DeptId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted=await departementServices.DeleteDepartment(DeptId);
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
            return RedirectToAction(nameof(Delete),new {Id= DeptId});
        }


        #endregion
    }

}
