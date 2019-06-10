using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Links;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.DomainOperations.Interfaces
{
    public interface IUserOperations
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User user, Department department);
        User UpdateUser(User user, Department department, string emailAddress);
        int? DeleteUser(User user);
        User ChangePassword(User user, string oldPassword, string newPassword);
        User ChangeUserContract(User user, Contract contract);
        IEnumerable<User> GetUsersInDepartment(Department department);
        IEnumerable<User> GetUsersWithBirthdayToday();
        void UpdateUserDepartment(User user, Department department);
        bool UserExists(int userId);
        bool UserHasContract(int userId);
        bool UserIsManager(User user);
    }
}
