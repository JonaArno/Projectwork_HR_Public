using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.User
{
    public class UpdatePasswordDto
    {
        /// <summary>
        /// Current password.
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// New password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
