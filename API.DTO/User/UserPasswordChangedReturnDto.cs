using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.User
{
    public class UserPasswordChangedReturnDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// New password of the user.
        /// </summary>
        public string PassWord { get; set; }
    }
}
