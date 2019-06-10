using System;
using System.Collections.Generic;
using System.Text;

namespace API.DTO.WorkTime
{
    public class UpdateWorkTimeDto
    {
        /// <summary>
        /// New work time datetime information.
        /// </summary>
        public DateTime UpdatedWorkTime { get; set; }
    }
}
