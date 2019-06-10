using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using API.Data;
using API.DomainOperations.Interfaces;
using API.DomainOperations.JSONRequest;
using API.DTO.Links;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace API.DomainOperations
{
    public class UserOperations : IUserOperations
    {
        private readonly HrApplicationContext _context;
        private readonly IConfiguration _configuration;

        public UserOperations(HrApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users
                    .Include(u => u.Department)
                    .Include(u => u.Contract)
                    .Include(u => u.Holidays)
                    .Include(u => u.WorkTimes)
                    .ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users
                .Include(u => u.Department)
                .Include(u => u.Contract)
                .Include(u => u.Holidays)
                .Include(u => u.WorkTimes)
                .First(u => u.UserID == id);
        }

        public User CreateUser(User user, Department department)
        {
            user.Department = department;
            user.CreationDate = DateTime.Now;
            user.LastModified = DateTime.Now;
            _context.Users.Add(user);
            if (!Save()) throw new Exception($"Creating new user {user.Name} failed.");
            using (var client = new HttpClient())
            {
                var requestUrl = _configuration["LogicAppConnections:CreateNewUser"];
                var request = new JsonMailCreationRequest
                {
                    EmailAddress = user.EmailAddress,
                    Password = user.PassWord
                };
                var jsonRequest = JsonConvert.SerializeObject(request, Formatting.Indented);
                var content = new StringContent(jsonRequest.ToString(), Encoding.UTF8, "application/json");
                client.PostAsync(requestUrl, content);
            }
            return user;
        }

        public User UpdateUser(User user, Department newDepartment, string newEmailAddress)
        {
            user.Department = newDepartment;
            user.LastModified = DateTime.Now;

            if (!string.IsNullOrEmpty(newEmailAddress) && newEmailAddress != user.EmailAddress)
            {
                user.EmailAddress = newEmailAddress;
                user.LastModified = DateTime.Now;
            }
            _context.Users.Update(user);
            if (!Save()) throw new Exception($"Update for user with id {user.UserID} failed.");
            return user;
        }

        public int? DeleteUser(User user)
        {
            var userId = user.UserID;
            _context.Users.Remove(user);
            if (!Save()) throw new Exception($"Removal of user with id {user.UserID} failed while persisting.");
            return userId;
        }
        
        public User ChangePassword(User user, string oldPassword, string newPassword)
        {
            if (user.PassWord != oldPassword) return null;
            user.PassWord = newPassword;
            user.LastModified = DateTime.Now;
            _context.Users.Update(user);
            if (!Save()) throw new Exception($"Password change for {user.UserID} failed while persisting.");
            return user;
        }

        public User ChangeUserContract(User user, Contract contract)
        {
            user.Contract = contract;
            user.LastModified = DateTime.Now;
            _context.Users.Update(user);
            if (!Save()) throw new Exception($"Contract update for {user.UserID} failed while persisting.");
            return _context.Users
                .Include(u => u.Contract)
                .First(u => u == user);
        }

        public IEnumerable<User> GetUsersInDepartment(Department department)
        {
            return _context.Users.Where(u => u.Department == department)
                .ToList();
        }

        public void UpdateUserDepartment(User user, Department dep)
        {
            user.Department = dep;
            _context.Users.Update(user);
            if (!Save()) throw new Exception($"Issue while updating user department for {user.UserID}.");
        }

        public IEnumerable<User> GetUsersWithBirthdayToday()
        {
            return _context.Users.Where(
                    u => u.BirthDay.Day == DateTime.Now.Day &&
                         u.BirthDay.Month == DateTime.Now.Month)
                .ToList();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UserID == userId);
        }

        public bool UserHasContract(int userId)
        {
            return _context.Contracts.Any(cont => cont.User.UserID == userId);
        }

        public bool UserIsManager(User user)
        {
            return _context.Departments.Any(dep => dep.Manager == user);
        }

        private bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
