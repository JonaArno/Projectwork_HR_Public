using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Department;

namespace API.DTO.User
{
    public class UserChangedReturnDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Department information of the user.
        /// </summary>
        public DepartmentReturnMinifiedDto Department { get; set; }
    }
}
