using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;
using API.DomainOperations.Interfaces;
using API.DomainServices.Interfaces;
using API.DTO;
using API.DTO.Contract;
using API.DTO.Holiday;
using API.DTO.Links;
using API.DTO.User;
using API.DTO.WorkTime;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContractOperations _contractOperations;
        private readonly IHolidayOperations _holidayOperations;
        private readonly IUserOperations _userOperations;
        private readonly IWorkTimeOperations _workTimeOperations;
        private readonly IDepartmentOperations _departmentOperations;

        public UserService(IContractOperations contractOperations, IHolidayOperations holidayOperations, IUserOperations userOperations, IWorkTimeOperations workTimeOperations, IDepartmentOperations departmentOperations, IUrlHelper urlHelper)
        {
            _contractOperations = contractOperations;
            _holidayOperations = holidayOperations;
            _userOperations = userOperations;
            _workTimeOperations = workTimeOperations;
            _departmentOperations = departmentOperations;
            _urlHelper = urlHelper;
        }

        public IEnumerable<UserReturnDto> GetAllUsers()
        {
            var allUsers = _userOperations.GetAllUsers();
            return Mapper.Map<IEnumerable<UserReturnDto>>(allUsers);

        }

        public UserReturnDto GetUserById(int id)
        {
            return Mapper.Map<UserReturnDto>(_userOperations.GetUserById(id));
        }


        public Tuple<UserReturnDto, LinkDto> CreateUser(NewUserDto user)
        {
            var department = _departmentOperations.GetDepartmentById(user.DepartmentId);
            if (department == null) return null;
            var returnedUser = _userOperations.CreateUser(Mapper.Map<User>(user), department);
            return new Tuple<UserReturnDto, LinkDto>(Mapper.Map<UserReturnDto>(returnedUser), CreateLink(returnedUser.UserID,"GetUserById",this._urlHelper));
        }

        public UserChangedReturnDto UpdateUser(int userId, UpdateUserDto userUpdate)
        {
            if (!_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            if (!_departmentOperations.DepartmentExists(userUpdate.DepartmentId)) return null;
            var department = _departmentOperations.GetDepartmentById(userUpdate.DepartmentId);
            var updatedUser = _userOperations.UpdateUser(user, department, userUpdate.EmailAddress);
            return updatedUser == null ? null : Mapper.Map<UserChangedReturnDto>(updatedUser);
        }

        public UserDeletedReturnDto DeleteUserById(int userId)
        {
            if (!_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            if (_userOperations.UserIsManager(user))
            {
                _departmentOperations.RemoveManager(user.Department);
            }
            var userDeleted = _userOperations.DeleteUser(user);
            return userDeleted == null ? null : Mapper.Map<UserDeletedReturnDto>(userDeleted);
        }

        public UserPasswordChangedReturnDto ChangePassword(int userId, UpdatePasswordDto passWords)
        {
            if (_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            if (passWords.OldPassword == passWords.NewPassword) return null;
            var changedUser = _userOperations.ChangePassword(user, passWords.OldPassword, passWords.NewPassword);
            return changedUser == null ? null : Mapper.Map<UserPasswordChangedReturnDto>(changedUser);
        }

        public ContractReturnDto GetContractByUserId(int userId)
        {
            if (_userOperations.UserExists(userId)) return null;
            var contractFound = _contractOperations.GetContractByUserId(userId);
            return contractFound == null ? null : Mapper.Map<ContractReturnDto>(contractFound);
        }

        public UserContractUpdatedReturnDto ChangeUserContract(int userId, int newContractId)
        {
            if (!_userOperations.UserExists(userId) || !_contractOperations.ContractExists(newContractId)) return null;
            var user = _userOperations.GetUserById(userId);
            var contract = _contractOperations.GetContractById(newContractId);
            if (contract.HasAssociatedUser()) return null;
            var updatedUser = _userOperations.ChangeUserContract(user, contract);
            return updatedUser == null ? null : Mapper.Map<UserContractUpdatedReturnDto>(updatedUser);
        }

        public UserContractRemovedReturnDto RemoveContractFromUser(int userId)
        {
            if (!_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            if (!user.HasContract()) return null;
            var updatedUser = _contractOperations.RemoveContractFromUser(user);
            return updatedUser == null ? null : Mapper.Map<UserContractRemovedReturnDto>(updatedUser);
        }

        
        public IEnumerable<UserReturnMinifiedDto> GetUsersWithBirthdayToday()
        {
            return Mapper.Map<IEnumerable<UserReturnMinifiedDto>>(_userOperations.GetUsersWithBirthdayToday());
        }

        public IEnumerable<HolidayReturnMinifiedDto> GetHolidaysForYearForUser(int userId, int year)
        {
            if (!_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            return Mapper.Map<IEnumerable<HolidayReturnMinifiedDto>>(
                _holidayOperations.GetAllHolidaysForUserForYear(user, year));
        }

        public IEnumerable<WorkTimeReturnMinifiedDto> GetWorkTimeOverviewForPeriod(int userId, DateTime startDateTime, DateTime endDateTime)
        {
            if (!_userOperations.UserExists(userId)) return null;
            var user = _userOperations.GetUserById(userId);
            var workTimesForPeriod =
                _workTimeOperations.GetWorkTimeOverViewForUserForPeriod(user, startDateTime, endDateTime);
            return workTimesForPeriod == null ? null : Mapper.Map<IEnumerable<WorkTimeReturnMinifiedDto>>(workTimesForPeriod);
        }
    }
}
