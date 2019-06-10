using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Department
{
    public class DepartmentReturnMinifiedDto
    {
        /// <summary>
        /// The unique identifier of the department.
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// The name of the department.
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
