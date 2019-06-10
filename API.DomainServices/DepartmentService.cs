using System;
using System.Collections.Generic;
using System.Text;
using API.DomainOperations;
using API.DomainOperations.Interfaces;
using API.DomainServices.Interfaces;
using API.DTO.Department;
using API.DTO.Links;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices
{
    public class DepartmentService : AbstractService, IDepartmentService
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContractOperations _contractOperations;
        private readonly IHolidayOperations _holidayOperations;
        private readonly IUserOperations _userOperations;
        private readonly IWorkTimeOperations _workTimeOperations;
        private readonly IDepartmentOperations _departmentOperations;

        public DepartmentService(IContractOperations contractOperations, IHolidayOperations holidayOperations, IUserOperations userOperations, IWorkTimeOperations workTimeOperations, IDepartmentOperations departmentOperations, IUrlHelper urlHelper)
        {
            _contractOperations = contractOperations;
            _holidayOperations = holidayOperations;
            _userOperations = userOperations;
            _workTimeOperations = workTimeOperations;
            _departmentOperations = departmentOperations;
            _urlHelper = urlHelper;
        }


        public IEnumerable<DepartmentReturnDto> GetAllDepartments()
        {
            return Mapper.Map<IEnumerable<DepartmentReturnDto>>(_departmentOperations.GetAllDepartments());
        }

        public DepartmentReturnDto GetDepartmentById(int id)
        {
            if (!_departmentOperations.DepartmentExists(id)) return null;
            var department = _departmentOperations.GetDepartmentById(id);
            return department == null ? null : Mapper.Map<DepartmentReturnDto>(department);
        }

        public Tuple<DepartmentReturnDto, LinkDto> CreateDepartment(CreateDepartmentDto newDepartment)
        {
            if (!_userOperations.UserExists(newDepartment.ManagerId)) return null;
            var user = _userOperations.GetUserById(newDepartment.ManagerId);
            var createdDepartment =_departmentOperations.CreateDepartment(newDepartment.DepartmentName, user);
            _userOperations.UpdateUserDepartment(user, createdDepartment);
            return new Tuple<DepartmentReturnDto, LinkDto>(Mapper.Map<DepartmentReturnDto>(createdDepartment), CreateLink(createdDepartment.DepartmentID, "GetDepartmentById",this._urlHelper));
        }

        public DepartmentReturnDto UpdateDepartmentName(int id, UpdateDepartmentDto updateDepartment)
        {
            return !_departmentOperations.DepartmentExists(id) ? null : Mapper.Map<DepartmentReturnDto>(_departmentOperations.UpdateDepartmentName(_departmentOperations.GetDepartmentById(id), updateDepartment.NewDepartmentName));
        }

        public void DeleteDepartment(int id)
        {
            if (!_departmentOperations.DepartmentExists(id)) return;
            _departmentOperations.DeleteDepartment(_departmentOperations.GetDepartmentById(id));
        }
    }
}
