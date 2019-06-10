using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.User;

namespace API.DTO
{
    public class HolidayReturnDto
    {
        /// <summary>
        /// The unique identifier of the holiday.
        /// </summary>
        public int HolidayId { get; set; }
        /// <summary>
        /// Start datetime of the holiday period.
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// End datetime of the holiday period.
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// The approval status of the holiday.
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
