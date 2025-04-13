using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;

namespace IKEA.BLL.Services.DepartmentServise
{
    public interface IDepartementServices
    {
        IEnumerable<DepartmentDto> GetAllDepartments();

        Task <DepartmentDetailsDto>? GetDepartmentById(int id);

        Task<int> CreateDepartment(CreatedDepartmentDto departmentDto);

        Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto);

        Task<bool> DeleteDepartment(int id);
        
    }
}
