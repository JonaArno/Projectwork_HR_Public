using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Department;
using API.DTO.Links;

namespace API.DomainServices.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentReturnDto> GetAllDepartments();
        DepartmentReturnDto GetDepartmentById(int id);
        Tuple<DepartmentReturnDto, LinkDto> CreateDepartment(CreateDepartmentDto newDepartment);
        DepartmentReturnDto UpdateDepartmentName(int id, UpdateDepartmentDto updateDepartment);
        void DeleteDepartment(int id);
    }
}
