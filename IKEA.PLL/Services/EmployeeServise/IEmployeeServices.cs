using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;

namespace IKEA.BLL.Services.EmployeeServise
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDetailsDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto employeeDto);

        int UpdateDepartment(UpdatedEmployeeDto employeeDto);

        bool DeleteEmployee(int id);
    }
}
