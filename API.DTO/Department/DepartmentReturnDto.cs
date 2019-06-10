using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.User;

namespace API.DTO.Department
{
    public class DepartmentReturnDto
    {
        /// <summary>
        /// The unique identifier of the department.
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// The name of the department.
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Manager of the department
        /// </summary>
        public ManagerReturnDto Manager { get; set; }

    }
}
