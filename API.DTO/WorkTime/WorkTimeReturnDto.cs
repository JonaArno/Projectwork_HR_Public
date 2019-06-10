using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.User;

namespace API.DTO.WorkTime
{
    public class WorkTimeReturnDto
    {
        /// <summary>
        /// Unique identifier of this work time registration.
        /// </summary>
        public int WorkTimeId { get; set; }
        /// <summary>
        /// Time value associated with this time registration.
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// User associated with this work time registration.
        /// </summary>
        public UserReturnMinifiedDto User { get; set; }
    }
}
