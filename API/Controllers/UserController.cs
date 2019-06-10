
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.DomainServices.Interfaces;
using API.DTO;
using API.DTO.Contract;
using API.DTO.Holiday;
using API.DTO.User;
using API.DTO.WorkTime;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/users")]
    public class UserController : AbstractController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets all users currently present in the database.
        /// </summary>
        /// <returns>Collection of all users in the database. Will return an empty collection if none present.</returns>
        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserReturnDto>), 200)]
        public IActionResult GetAllUsers()
        {
            var allUsers = _userService.GetAllUsers();
            return Ok(allUsers);
        }

        /// <summary>
        /// Gets the user by their identifier.
        /// </summary>
        /// <returns>The requested user.</returns>
        /// <param name="id">Unique identifier of the user.</param>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int id)
        {
            return OkOrNotFound(_userService.GetUserById(id));
        }

        /// <summary>
        /// Gets a list of users who have their birthdays today
        /// </summary>
        /// <returns>A list of users whose birthday is today.</returns>
        [HttpGet("birthdays", Name = "GetUsersWithBirthdayToday")]
        [ProducesResponseType(typeof(UserReturnMinifiedDto), 200)]
        public IActionResult GetUsersWithBirthdayToday()
        {
            return Ok(_userService.GetUsersWithBirthdayToday());
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">Object containing the details of the to-be-created user</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] NewUserDto newUser)
        {
            if (newUser == null) return BadRequest();
            var link = _userService.CreateUser(newUser);
            if (link == null) return BadRequest();
            return CreatedAtRoute("GetUserById", new { id = link.Item2.Identifier }, link.Item1);
        }

        /// <summary>
        /// Update an existing user's department and/or email address.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="id">User identifier.</param>
        /// <param name="updatedUserValues">Changed values for department and/or email address.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserChangedReturnDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updatedUserValues)
        {
            var userUpdated = _userService.UpdateUser(id, updatedUserValues);
            return OkOrBadRequest(userUpdated);
        }

        /// <summary>
        /// Update the password for an existing user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="passwords">Object containing both the existing and new password for the user.</param>
        /// <returns></returns>
        [HttpPut("{id}/password")]
        [ProducesResponseType(typeof(UserPasswordChangedReturnDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult ChangePassword(int id, [FromBody] UpdatePasswordDto passwords)
        {
            if (passwords == null) return BadRequest();
            var passwordUpdated = _userService.ChangePassword(id, passwords);
            return OkOrBadRequest(passwordUpdated);
        }

        /// <summary>
        /// Remove a user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeleteUserById")]
        [ProducesResponseType(typeof(UserDeletedReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserById(int id)
        {
            var userDeleted = _userService.DeleteUserById(id);
            return OkOrNotFound(userDeleted);
        }

        /// <summary>
        /// Returns a collection of the holidays for a specific year for a specific user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="year">The year for which the user's holidays should be retrieved</param>
        /// <returns></returns>
        [HttpGet("{id}/holidays/{year}", Name = "GetHolidaysForYearForUser")]
        [ProducesResponseType(typeof(HolidayReturnMinifiedDto), 200)]
        public IActionResult GetHolidaysForYearForUser(int id, int year)
        {
            return Ok(_userService.GetHolidaysForYearForUser(id, year));
        }

        /// <summary>
        /// Request the contract associated with a specific user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns></returns>
        [HttpGet("{id}/contract", Name = "GetUserContractById")]
        [ProducesResponseType(typeof(ContractReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetUserContractById(int id)
        {
            return OkOrNotFound(_userService.GetContractByUserId(id));
        }

        /// <summary>
        /// Update the current contract associated with an existing user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="newContractId">The identifier of the contract that should be associated with the user.</param>
        /// <returns></returns>
        [HttpPut("{id}/contract/{newContractId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult ChangeUserContract(int id, int newContractId)
        {
            return OkOrBadRequest(_userService.ChangeUserContract(id, newContractId));
        }

        /// <summary>
        /// Remove contract from a user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns></returns>
        [HttpDelete("{id}/contract")]
        [ProducesResponseType(typeof(UserContractRemovedReturnDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult RemoveContractFromUser(int id)
        {
            return OkOrBadRequest(_userService.RemoveContractFromUser(id));
        }

        /// <summary>
        /// Gets an overview of all work time registrations present in the system for a specific user between a range of dates.
        /// </summary>
        /// <returns>The work time overview for the user between two time periods.</returns>
        /// <param name="id">UserId to get overview for</param>
        /// <param name="startDateTime">Start date</param>
        /// <param name="endDateTime">End date</param>
        [HttpGet("{id}/worktimes")]
        [ProducesResponseType(typeof(IEnumerable<WorkTimeReturnDto>), 200)]
        public IActionResult GetWorkTimeOverviewForTimePeriodForUser(int id, DateTime startDateTime, DateTime endDateTime)
        {
            return Ok(_userService.GetWorkTimeOverviewForPeriod(id, startDateTime, endDateTime));
        }

    }
}
