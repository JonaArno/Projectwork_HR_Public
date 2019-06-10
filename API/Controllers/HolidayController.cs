using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.DomainServices;

using API.DomainServices.Interfaces;
using API.DTO;
using API.DTO.Holiday;
using API.DTO.User;

namespace API.Controllers
{
    [Route("api/holidays")]

    public class HolidayController : AbstractController
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        /// <summary>
        /// Gets all holidays in the database.
        /// </summary>
        /// <returns>A list of all registered holidays for all users present in the database.</returns>
        [HttpGet(Name = "GetAllHolidays")]
        [ProducesResponseType(typeof(IEnumerable<HolidayReturnMinifiedDto>), 200)]
        public IActionResult GetAllHolidays()
        {
            return Ok(_holidayService.GetAllHolidays());
        }

        /// <summary>
        /// Gets all holidays in the database for the provided year
        /// </summary>
        /// <param name="year">The year for which holidays should be returned.</param>
        /// <returns>A list of all registered holidays in the current year</returns>
        [HttpGet("{year}", Name = "GetHolidaysForYear")]
        [ProducesResponseType(typeof(IEnumerable<HolidayReturnMinifiedDto>), 200)]
        public IActionResult GetAllHolidaysForYear(int year)
        {
            return Ok(_holidayService.GetAllHolidaysForYear(year));
        }

        /// <summary>
        /// Gets a holiday by its unique identifier.
        /// </summary>
        /// <param name="holidayId">Unique identifier of the holiday.</param>
        /// <returns></returns>
        [HttpGet("{holidayId}", Name = "GetHolidayById")]
        [ProducesResponseType(typeof(HolidayReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetHolidayById(int holidayId)
        {
            return OkOrNotFound(_holidayService.GetHolidayById(holidayId));
        }

        /// <summary>
        /// Request a holiday.
        /// </summary>
        /// <param name="userId">Unique identifier of the user requesting a holiday.</param>
        /// <param name="startDate">Start date of the holiday.</param>
        /// <param name="endDate">End date of the holiday.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RequestHoliday(int userId, DateTime startDate, DateTime endDate)
        {
            var requestedHoliday = _holidayService.RequestHoliday(userId, startDate, endDate);
            if (requestedHoliday == null) return BadRequest();
            return CreatedAtRoute("GetHolidayById", new { holidayId = requestedHoliday.Item2.Identifier }, requestedHoliday.Item1);
        }

        /// <summary>
        /// Update the dates of a holiday.
        /// </summary>
        /// <param name="id">Unique identifier of the holiday.</param>
        /// <param name="updatedHoliday">Requested changes to the holiday.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HolidayReturnDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateHoliday(int id, [FromBody] UpdateHolidayDto updatedHoliday)
        {
            return OkOrBadRequest(_holidayService.UpdateHoliday(id, updatedHoliday));
        }

        /// <summary>
        /// Gets a list of holidays for a manager to approve.
        /// </summary>
        /// <returns>The holidays to approve.</returns>
        /// <param name="managerUserId">Manager user identifier.</param>
        [HttpGet("approve")]
        [ProducesResponseType(typeof(IEnumerable<HolidayReturnDto>),200)]
        [ProducesResponseType(400)]
        public IActionResult GetHolidaysToApprove(int managerUserId)
        {
            return OkOrBadRequest(_holidayService.GetHolidaysToApprove(managerUserId));
        }

        /// <summary>
        /// Approve a holiday.
        /// </summary>
        /// <param name="holidayId">Unique identifier of the holiday to approve.</param>
        /// <param name="managerUserId">Unique identifier of the approving manager.</param>
        /// <returns></returns>
        [HttpPut("{holidayId}/approve/{managerUserId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult ApproveHoliday(int holidayId, int managerUserId)
        {
            return OkOrBadRequest(_holidayService.ApproveHoliday(holidayId, managerUserId));
        }

        /// <summary>
        /// Remove a holiday.
        /// </summary>
        /// <param name="id">Unique identifier of the holiday to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult RemoveHoliday(int id)
        {
            _holidayService.DeleteHoliday(id);
            return Ok();
        }
    }
}
