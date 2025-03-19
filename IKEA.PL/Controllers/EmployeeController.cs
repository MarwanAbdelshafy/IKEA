using IKEA.BLL.Services.EmployeeServise;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Servicse-DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion
        #region Index

        public IActionResult Index()
        {
            return View();
        } 
        #endregion
    }
}
