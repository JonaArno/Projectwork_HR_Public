using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.User
{
    public class UserReturnMinifiedDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string Name { get; set; }

    }
}
