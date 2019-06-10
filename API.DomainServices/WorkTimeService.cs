using System;
using System.Collections.Generic;
using System.Text;
using API.DomainOperations.Interfaces;
using API.DomainServices.Interfaces;
using API.DTO.Links;
using API.DTO.WorkTime;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices
{
    public class WorkTimeService : AbstractService, IWorkTimeService
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContractOperations _contractOperations;
        private readonly IHolidayOperations _holidayOperations;
        private readonly IUserOperations _userOperations;
        private readonly IWorkTimeOperations _workTimeOperations;
        private readonly IDepartmentOperations _departmentOperations;

        public WorkTimeService(IContractOperations contractOperations, IHolidayOperations holidayOperations, IUserOperations userOperations, IWorkTimeOperations workTimeOperations, IDepartmentOperations departmentOperations, IUrlHelper urlHelper)
        {
            _contractOperations = contractOperations;
            _holidayOperations = holidayOperations;
            _userOperations = userOperations;
            _workTimeOperations = workTimeOperations;
            _departmentOperations = departmentOperations;
            _urlHelper = urlHelper;
        }

        public IEnumerable<WorkTimeReturnDto> GetAllWorkTimes()
        {
            return Mapper.Map<IEnumerable<WorkTimeReturnDto>>(_workTimeOperations.GetAllWorkTimes());
        }

        public WorkTimeReturnDto GetWorkTimeById(int id)
        {
            return Mapper.Map<WorkTimeReturnDto>(_workTimeOperations.GetWorkTimeById(id));
        }

        public IEnumerable<WorkTimeReturnDto> GetWorkTimesForTimePeriod(DateTime startDateTime, DateTime endDateTime)
        {
            return Mapper.Map<IEnumerable<WorkTimeReturnDto>>(
                _workTimeOperations.GetWorkTimesForTimePeriod(startDateTime, endDateTime));
        }

        public Tuple<WorkTimeReturnMinifiedDto, LinkDto> RegisterWorkTime(int userId)
        {
            if (!_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            var registeredWorkTime = _workTimeOperations.RegisterWorkTime(user);
            return registeredWorkTime == null
                ? null
                : new Tuple<WorkTimeReturnMinifiedDto, LinkDto>(Mapper.Map<WorkTimeReturnMinifiedDto>(registeredWorkTime),
                    CreateLink(registeredWorkTime.WorkTimeID, "GetWorkTimeById",this._urlHelper));
        }

        public WorkTimeReturnDto CorrectWorkTime(int workTimeId, UpdateWorkTimeDto updatedWorkTime)
        {
            if (!_workTimeOperations.IsValidWorkTime(workTimeId)) return null;
            var workTimeRegistration = _workTimeOperations.GetWorkTimeById(workTimeId);
            return Mapper.Map<WorkTimeReturnDto>(
                _workTimeOperations.CorrectWorkTime(workTimeRegistration, updatedWorkTime.UpdatedWorkTime));
        }

        public void DeleteWorkTime(int workTimeId)
        {
            if (!_workTimeOperations.IsValidWorkTime(workTimeId)) return;
            var workTimeRegistration = _workTimeOperations.GetWorkTimeById(workTimeId);
            _workTimeOperations.DeleteWorkTime(workTimeRegistration);
        }
    }
}
