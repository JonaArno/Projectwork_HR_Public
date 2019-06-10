using System;
using System.Collections.Generic;
using System.Text;
using API.Model;
using API.DTO;
using API.DTO.Contract;
using API.DTO.Holiday;
using API.DTO.Links;
using API.DTO.User;
using API.DTO.WorkTime;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainServices.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserReturnDto> GetAllUsers();
        UserReturnDto GetUserById(int id);
        Tuple<UserReturnDto,LinkDto> CreateUser(NewUserDto newUser);
        UserChangedReturnDto UpdateUser(int userId, UpdateUserDto user);
        UserDeletedReturnDto DeleteUserById(int userId);
        UserPasswordChangedReturnDto ChangePassword(int userId, UpdatePasswordDto passWords);
        ContractReturnDto GetContractByUserId(int userId);
        UserContractUpdatedReturnDto ChangeUserContract(int userId, int newContractId);
        UserContractRemovedReturnDto RemoveContractFromUser(int userId);
        IEnumerable<UserReturnMinifiedDto> GetUsersWithBirthdayToday();
        IEnumerable<HolidayReturnMinifiedDto> GetHolidaysForYearForUser(int userId, int year);
        IEnumerable<WorkTimeReturnMinifiedDto> GetWorkTimeOverviewForPeriod(int userId, DateTime startDateTime,
            DateTime endDateTime);
    }
}
