using System;
using System.Collections.Generic;
using System.Text;
using API.DomainOperations.Interfaces;
using API.DomainServices.Interfaces;
using API.DTO;
using API.DTO.Holiday;
using API.DTO.Links;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices
{
    public class HolidayService : AbstractService, IHolidayService
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContractOperations _contractOperations;
        private readonly IHolidayOperations _holidayOperations;
        private readonly IUserOperations _userOperations;
        private readonly IWorkTimeOperations _workTimeOperations;
        private readonly IDepartmentOperations _departmentOperations;

        public HolidayService(IContractOperations contractOperations, IHolidayOperations holidayOperations, IUserOperations userOperations, IWorkTimeOperations workTimeOperations, IDepartmentOperations departmentOperations, IUrlHelper urlHelper)
        {
            _contractOperations = contractOperations;
            _holidayOperations = holidayOperations;
            _userOperations = userOperations;
            _workTimeOperations = workTimeOperations;
            _departmentOperations = departmentOperations;
            _urlHelper = urlHelper;
        }

        public IEnumerable<HolidayReturnDto> GetAllHolidays()
        {
            return Mapper.Map<IEnumerable<HolidayReturnDto>>(_holidayOperations.GetAllHolidays());
        }

        public IEnumerable<HolidayReturnDto> GetAllHolidaysForYear(int year)
        {
            return Mapper.Map<IEnumerable<HolidayReturnDto>>(_holidayOperations.GetAllHolidaysForYear(year));
        }

        public HolidayReturnDto GetHolidayById(int id)
        {
            var holiday = _holidayOperations.GetHolidayById(id);
            return holiday == null ? null : Mapper.Map<HolidayReturnDto>(holiday);
        }

        public Tuple<HolidayCreatedReturnDto, LinkDto> RequestHoliday(int userId, DateTime startDateTime, DateTime endDateTime)
        {
            if (!_userOperations.UserExists(userId)) return null;
            if (!_userOperations.UserHasContract(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            var isManager = _userOperations.UserIsManager(user);
            var createdHoliday = _holidayOperations.RequestHoliday(user, startDateTime, endDateTime, isManager);
            var returnDto = Mapper.Map<HolidayCreatedReturnDto>(createdHoliday);
            returnDto.NumberOfDays = createdHoliday.NumberOfWorkDays();
            return new Tuple<HolidayCreatedReturnDto, LinkDto>(returnDto, CreateLink(createdHoliday.ID, "GetHolidayById", this._urlHelper));
        }

        public HolidayReturnDto UpdateHoliday(int id, UpdateHolidayDto updatedHoliday)
        {
            if (!_holidayOperations.HolidayExists(id)) return null;
            var holidayToUpdate = _holidayOperations.GetHolidayById(id);
            var user = holidayToUpdate.User;
            var isManager = _userOperations.UserIsManager(user);
            return Mapper.Map<HolidayReturnDto>(_holidayOperations.UpdateHoliday(holidayToUpdate, updatedHoliday.NewStartDateTime,
                updatedHoliday.NewEndDateTime, isManager));
        }

        public void DeleteHoliday(int id)
        {
            if (!_holidayOperations.HolidayExists(id)) return;
            _holidayOperations.RemoveHoliday(_holidayOperations.GetHolidayById(id));
        }

        public IEnumerable<HolidayReturnDto> GetHolidaysToApprove(int managerId)
        {
            if (!_userOperations.UserExists(managerId)) return null;
            var manager = _userOperations.GetUserById(managerId);
            if (!_userOperations.UserIsManager(manager)) return null;
            var department = _departmentOperations.GetDepartmentOfManager(manager);
            var usersInDepartment = _userOperations.GetUsersInDepartment(department);
            return Mapper.Map<IEnumerable<HolidayReturnDto>>(_holidayOperations.GetUnapprovedHolidays(usersInDepartment));
        }

        public HolidayReturnDto ApproveHoliday(int holidayId, int managerId)
        {
            if (!_holidayOperations.HolidayExists(holidayId) || !_userOperations.UserExists(managerId)) return null;

            var holiday = _holidayOperations.GetHolidayById(holidayId);
            var user = _userOperations.GetUserById(managerId);

            if (holiday.IsApproved || !user.IsManagerOfDepartment()) return null;

            return Mapper.Map<HolidayReturnDto>(_holidayOperations.ApproveHoliday(holiday));
        }
    }
}
