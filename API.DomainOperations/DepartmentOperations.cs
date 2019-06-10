using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Data;
using API.DomainOperations.Interfaces;
using API.DTO.Links;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.DomainOperations
{
    public class DepartmentOperations : IDepartmentOperations
    {
        private readonly HrApplicationContext _context;

        public DepartmentOperations(HrApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments
                .Include(dep => dep.Manager)
                .ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments
                .Include(dep => dep.Manager)
                .First(dep => dep.DepartmentID == id);
        }

        public Department CreateDepartment(string departmentName, User manager)
        {
            var newDep = new Department
            {
                DepartmentName = departmentName,
                Manager = manager,
                CreationDate = DateTime.Now,
                LastModified = DateTime.Now
            };
            _context.Departments.Add(newDep);
            if (!Save()) throw new Exception($"Problem while creating department {departmentName}.");
            return newDep;
        }

        public Department UpdateDepartmentName(Department department, string newDepartmentName)
        {
            department.DepartmentName = newDepartmentName;
            department.LastModified = DateTime.Now;
                _context.Departments.Update(department);
            if (!Save()) throw new Exception($"Update of department name for department with id {department.DepartmentID} failed.");
            return department;
        }

        public void DeleteDepartment(Department depToDelete)
        {
            depToDelete.Manager = null;
            _context.Departments.Update(depToDelete);
            _context.SaveChanges();
            var usersWithDepartment = _context.Users.Where(u => u.Department == depToDelete).ToList();
            foreach (var user in usersWithDepartment)
            {
                user.Department = null;
                user.LastModified = DateTime.Now;
                _context.Users.Update(user);
            }
            _context.SaveChanges();
            _context.Departments.Remove(depToDelete);
            if (!Save()) throw new Exception($"Removal op department with id {depToDelete.DepartmentID} failed while persisting to the database.");
        }

        public Department GetDepartmentOfManager(User manager)
        {
            return _context.Departments.FirstOrDefault(dep => dep.Manager == manager);
        }
        
        public bool DepartmentExists(int departmentId)
        {
            return _context.Departments.Any(dep => dep.DepartmentID == departmentId);
        }

        public void RemoveManager(Department department)
        {
            department.Manager = null;
            _context.Departments.Update(department);
            if (!Save()) throw new Exception($"Problem while removing manager from department {department.DepartmentName}.");
        }

        private bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
