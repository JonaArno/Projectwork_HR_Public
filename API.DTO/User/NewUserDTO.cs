using System;

namespace API.DTO.User
{
    public class NewUserDto
    {
        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Birth date of the user.
        /// </summary>
        public DateTime Birthdate { get; set; }
        /// <summary>
        /// Unique identifier of the user's department.
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
