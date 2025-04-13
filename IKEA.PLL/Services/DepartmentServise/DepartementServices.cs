using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Department;
using IKEA.DAL.Persistance.Reposatories.Departments;
using IKEA.DAL.Persistance.UnitOfWork;

namespace IKEA.BLL.Services.DepartmentServise
{
    public class DepartementServices:IDepartementServices
    {
       // private IDepartmentReposatory Repository;
        private readonly IUnitOfWork unitOfWork;

        public DepartementServices(IUnitOfWork unitOfWork)
        {
            //Repository = _reposatory;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = unitOfWork.departmentReposatory.GetAll().Select(dept => new DepartmentDto
            {

                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
            }).ToList();

            return Departments;
        }

        public async Task<DepartmentDetailsDto?> GetDepartmentById(int id)
        {
            var Department = await unitOfWork.departmentReposatory.GetById(id);

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
        public async Task<int> CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var  CreatedDepartment = new Departmentt()
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
            unitOfWork.departmentReposatory.Add(CreatedDepartment);
            return await unitOfWork.complete();
        }

        public async Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var updatedDepartment = new Departmentt()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now
            };
            unitOfWork.departmentReposatory.Update(updatedDepartment);
            return await unitOfWork.complete();
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            var department =await unitOfWork.departmentReposatory.GetById(id);
            //int result = 0; 
            if (department is not null)
                 unitOfWork.departmentReposatory.Delete(department) ;

            if (await unitOfWork.complete() > 0)
                return true;
            else
                return false;
        }

    }
}
