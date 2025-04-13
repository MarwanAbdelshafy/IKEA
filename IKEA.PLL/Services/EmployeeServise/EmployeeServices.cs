using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Common.Enum_s;
using IKEA.DAL.Models.Employees;

using Microsoft.EntityFrameworkCore;
using IKEA.DAL.Persistance.Reposatories.Employee;
using IKEA.DAL.Persistance.UnitOfWork;
using IKEA.BLL.Common.services.Attachments;

namespace IKEA.BLL.Services.EmployeeServise
{
    public class EmployeeServices :IEmployeeServices
    {
        //private readonly IEmployeeReposatory reposatory;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAttachmetService attachmetService;

        public EmployeeServices(IUnitOfWork unitOfWork ,IAttachmetService attachmetService )
        {
            this.unitOfWork = unitOfWork;
            this.attachmetService = attachmetService;
        }

        public async Task<IQueryable<EmployeeDto>> GetAllEmployees(string? Search)
        {

            var employees = unitOfWork.employeeReposatory.GetAll();

            var queryEmployees = employees.Where(E => !E.IsDeleted &&  (string.IsNullOrEmpty(Search)
            || E.Name.ToLower().Contains(Search.ToLower())))
                .Include(E => E.Department)
                .Select(E => new EmployeeDto
            {

                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType,
                departments= E.Department.Name ?? "N/A"
                
            });
            return queryEmployees;

        } 


        //public IEnumerable<EmployeeDto> GetAllEmployees()
        //{
        //    return reposatory.GetAll().Where(E => E.IsDeleted == false).Select(Emp => new EmployeeDto
        //    {

        //        Id = Emp.Id,
        //        Name = Emp.Name,
        //        Age = Emp.Age,
        //        Salary = Emp.Salary,
        //        IsActive = Emp.IsActive,
        //        Email = Emp.Email,
        //        Gender = Emp.Gender,
        //        EmployeeType = Emp.EmployeeType,
        //        departments = Emp.Department.Name ?? "N/A"

        //    }).ToList();


        //}

        public async Task<EmployeeDetailsDto>? GetEmployeeById(int id)
        {
            var employee =await unitOfWork.employeeReposatory.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    departments = employee.Department?.Name ?? "N/A",

                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    ImageName = employee.ImageName,
                };
            }
            return null;
        }
        public async Task<int> CreateEmployee(CreatedEmployeeDto employeeDto)
        {
 
            var employee = new Employeess()
            {
                Name = employeeDto.name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn=DateTime.Now,
                CreatedOn= DateTime.Now,

            };

            if (employeeDto.Image is not null)
            {
                employee.ImageName = attachmetService.UploadImage(employeeDto.Image, "images");
            }
            unitOfWork.employeeReposatory.Add(employee);
            return await unitOfWork.complete();
            
        }
     
        public async Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employeess()
            {
                Id= employeeDto.Id,
                Name = employeeDto.name,
                Age = employeeDto.Age,
                Address = employeeDto.Address, 
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                ImageName=employeeDto.ImasgeName,


                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };

            if (employeeDto.Image is not null)

            { 
                if (employeeDto.ImasgeName is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employee.ImageName);
                    attachmetService.Delete(filePath);
                }
            }
            employee.ImageName = attachmetService.UploadImage(employeeDto.Image, "images");


                unitOfWork.employeeReposatory.Update(employee);
            return await unitOfWork.complete();
        }
        public async Task<bool> DeleteEmployee(int id)
        {

            var employee =await unitOfWork.employeeReposatory.GetById(id);
            //int result = 0; 
            if (employee is not null)
            {

                if (employee.ImageName is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employee.ImageName);

                    attachmetService.Delete(filePath);
                }
                unitOfWork.employeeReposatory.Delete(employee); 

            
            }
            if ( await unitOfWork.complete() > 0)
                return true;

            else
                return false;
        }
    }
}
