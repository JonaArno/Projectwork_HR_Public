using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.Holiday
{
    public class UpdateHolidayDto
    {
        /// <summary>
        /// The new start datetime of the holiday to update.
        /// </summary>
        public DateTime NewStartDateTime { get; set; }
        /// <summary>
        /// The new start datetime of the holiday to update.
        /// </summary>
        public DateTime NewEndDateTime { get; set; }
    }
}
