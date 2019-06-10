using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.WorkTime
{
    public class WorkTimeReturnMinifiedDto
    {
        /// <summary>
        /// Unique identifier of this work time registration.
        /// </summary>
        public int WorkTimeId { get; set; }
        /// <summary>
        /// Time value associated with this time registration.
        /// </summary>
        public DateTime Time { get; set; }
    }
}
