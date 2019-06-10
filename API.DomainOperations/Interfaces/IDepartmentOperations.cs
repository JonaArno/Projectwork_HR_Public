using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Links;
using API.Model;

namespace API.DomainOperations.Interfaces
{
    public interface IDepartmentOperations
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        Department CreateDepartment(string departmentName, User manager);
        Department UpdateDepartmentName(Department department, string newDepartmentName);
        void DeleteDepartment(Department department);
        Department GetDepartmentOfManager(User manager);
        bool DepartmentExists(int id);
        void RemoveManager(Department department);
    }
}
