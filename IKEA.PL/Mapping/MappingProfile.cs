using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.PL.ViewModels;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentVM,CreatedDepartmentDto>().ReverseMap();
            CreateMap<DepartmentVM,UpdatedDepartmentDto>().ReverseMap();

        }
    }
}
