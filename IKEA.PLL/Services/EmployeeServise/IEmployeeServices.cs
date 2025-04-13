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
        Task <IQueryable<EmployeeDto>> GetAllEmployees(string Search);

        Task <EmployeeDetailsDto>? GetEmployeeById(int id);

        Task<int> CreateEmployee(CreatedEmployeeDto employeeDto);

        Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto);

        Task <bool> DeleteEmployee(int id);
    }
}
