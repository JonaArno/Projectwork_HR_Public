using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Department
{
    public class CreateDepartmentDto
    {
        /// <summary>
        /// The name of the department.
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// The unique identifier of the manager.
        /// </summary>
        public int ManagerId { get; set; }
    }
}