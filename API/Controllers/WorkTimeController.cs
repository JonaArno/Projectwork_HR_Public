using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.DomainServices.Interfaces;
using API.DTO.WorkTime;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/worktimes")]

    public class WorkTimeController : AbstractController
    {
        private readonly IWorkTimeService _workTimeService;

        public WorkTimeController(IWorkTimeService workTimeService)
        {
            _workTimeService = workTimeService;
        }

        /// <summary>
        /// Gets all worktimes for all users in the database
        /// </summary>
        /// <returns>A list of all worktimes for all users</returns>
        [HttpGet(Name = "GetAllWorkTimes")]
        [ProducesResponseType(typeof(IEnumerable<WorkTimeReturnDto>), 200)]
        public IActionResult GetAllWorkTimes()
        {
            var allWorkTimes = _workTimeService.GetAllWorkTimes();
            return Ok(allWorkTimes);
        }

        /// <summary>
        /// Get a work time by its identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the work time</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetWorkTimeById")]
        [ProducesResponseType(typeof(WorkTimeReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetWorkTimeById(int id)
        {
            return OkOrNotFound(_workTimeService.GetWorkTimeById(id));
        }
        

        /// <summary>
        /// Logs a work time entry. The current time will be used.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult LogWorkTimeEntry(int userId)
        {
            var entry = _workTimeService.RegisterWorkTime(userId);

            if (entry == null) return BadRequest();
            return CreatedAtRoute("GetWorkTimeById", new { id = entry.Item2.Identifier }, entry.Item1);
        }


        /// <summary>
        /// Correct the time of a previously registered time registration.
        /// </summary>
        /// <param name="workTimeId"></param>
        /// <param name="workTimeUpdate">JSON object containing the new DateTime value.</param>
        /// <returns></returns>
        [HttpPut("{workTimeId}")]
        [ProducesResponseType(typeof(WorkTimeReturnDto),200)]
        [ProducesResponseType(400)]
        public IActionResult MakeWorkTimeCorrection(int workTimeId, [FromBody] UpdateWorkTimeDto workTimeUpdate)
        {
            return OkOrBadRequest(_workTimeService.CorrectWorkTime(workTimeId, workTimeUpdate));
        }

        /// <summary>
        /// Remove a work time entry.
        /// </summary>
        /// <param name="workTimeId">Unique identifier of the entry to delete.</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public IActionResult DeleteWorkTimeEntry(int workTimeId)
        {
            _workTimeService.DeleteWorkTime(workTimeId);
            return Ok();
        }
    }
}
