using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Contract;
using API.DTO.Department;
using API.DTO.Holiday;
using API.DTO.WorkTime;
using API.Model;

namespace API.DTO.User
{
    public class UserReturnDto
    {
        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Birthday of the user.
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// Creation date of this user.
        /// </summary>
        public string CreationDate { get; set; }
        /// <summary>
        /// Collection of holidays for this user.
        /// </summary>
        public IEnumerable<HolidayReturnMinifiedDto> Holidays { get; set; }
        /// <summary>
        /// Collection of work time registrations of this user.
        /// </summary>
        public IEnumerable<WorkTimeReturnDto> WorkTimes { get; set; }
        /// <summary>
        /// Contract associated with this user.
        /// </summary>
        public ContractReturnDto Contract { get; set; }
        /// <summary>
        /// Department associated with this user.
        /// </summary>
        public DepartmentReturnMinifiedDto Department { get; set; }
        }
}
