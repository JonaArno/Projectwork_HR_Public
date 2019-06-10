using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.User
{
    public class ManagerReturnDto
    {
        /// <summary>
        /// Unique identifier of the manager.
        /// </summary>
        public int UserID { get; set;}
        /// <summary>
        /// The name of the manager.
        /// </summary>
        public string Name { get; set;}
        /// <summary>
        /// The email address of the manager.
        /// </summary>
        public string EmailAddress { get; set;}
    }
}
