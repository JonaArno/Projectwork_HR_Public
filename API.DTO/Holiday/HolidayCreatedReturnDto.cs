using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Holiday
{
    public class HolidayCreatedReturnDto
    {
        /// <summary>
        /// Start datetime of the holiday period.
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// End datetime of the holiday period
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// The total number of work days in the holiday period.
        /// </summary>
        public int NumberOfDays { get; set; }
        /// <summary>
        /// The approval status of the holiday request.
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
