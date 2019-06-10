using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainServices.Interfaces;
using API.DTO.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API.Controllers
{
    [Route("api/departments")]
    public class DepartmentController : AbstractController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Returns a collection of all departments.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllDepartments")]
        [ProducesResponseType(typeof(IEnumerable<DepartmentReturnDto>), 200)]
        public IActionResult GetAllDepartments()
        {
            return Ok(_departmentService.GetAllDepartments());
        }

        /// <summary>
        /// Get a department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of a department.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetDepartmentById")]
        [ProducesResponseType(typeof(DepartmentReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetDepartmentById(int id)
        {
            return OkOrNotFound(_departmentService.GetDepartmentById(id));
        }

        /// <summary>
        /// Create a new department.
        /// </summary>
        /// <param name="newDep">JSON object with information to create new department.</param>
        /// <returns></returns>
        [HttpPost(Name = "CreateDepartment")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] CreateDepartmentDto newDep)
        {
            var newDepartment = _departmentService.CreateDepartment(newDep);
            if (newDepartment == null) return BadRequest();
            return CreatedAtRoute("GetDepartmentById", new { id = newDepartment.Item2.Identifier }, newDepartment.Item1);
        }

        /// <summary>
        /// Update the name of an existing department
        /// </summary>
        /// <param name="id">Unique identifier of the department to update.</param>
        /// <param name="updateDep">JSON object containing the new name of the department.</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateDepartmentName")]
        [ProducesResponseType(typeof(DepartmentReturnDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartmentName(int id, [FromBody] UpdateDepartmentDto updateDep)
        {
            return OkOrNotFound(_departmentService.UpdateDepartmentName(id, updateDep));
        }

        /// <summary>
        /// Delete a department.
        /// </summary>
        /// <param name="id">Unique identifier of the to-be-deleted department.</param>
        /// <returns></returns>
        [HttpDelete("{id}",Name="DeleteDepartment")]
        [ProducesResponseType(200)]
        public IActionResult DeleteDepartment(int id)
        {
            _departmentService.DeleteDepartment(id);
            return Ok();
        }
    }
}
