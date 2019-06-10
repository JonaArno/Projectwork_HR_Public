using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.User
{
    public class UpdateUserDto
    {
        /// <summary>
        /// New email address of the user.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Unique identifier of the user's new department.
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
