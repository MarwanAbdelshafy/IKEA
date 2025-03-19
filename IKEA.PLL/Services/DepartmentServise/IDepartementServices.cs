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

        DepartmentDetailsDto? GetDepartmentById(int id);

        int CreateDepartment(CreatedDepartmentDto departmentDto);

        int UpdateDepartment(UpdatedDepartmentDto departmentDto);

        bool DeleteDepartment(int id);
        
    }
}
