using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Department;
using IKEA.DAL.Persistance.Reposatories.Departments;

namespace IKEA.BLL.Services.DepartmentServise
{
    public class DepartementServices:IDepartementServices
    {
        private IDepartmentReposatory Repository;

        public DepartementServices(IDepartmentReposatory _reposatory)
        {
            Repository = _reposatory;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = Repository.GetAll().Select(dept => new DepartmentDto
            {

                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
            }).ToList();

            return Departments;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = Repository.GetById(id);

            if (Department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    Description = Department.Description,
                    CreationDate = Department.CreationDate,
                    IsDeleted = Department.IsDeleted,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,
                    LastModifiedBy = Department.LastModifiedBy,

                };

            return null;
        }
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now
            };

            return Repository.Add(CreatedDepartment);
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var updatedDepartment = new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now
            };
            return Repository.Update(updatedDepartment);
        }
        public bool DeleteDepartment(int id)
        {
            var department = Repository.GetById(id);
            //int result = 0; 
            if (department is not null)
                return Repository.Delete(department) > 0;
            else
                return false;
        }
    }
}
