using System;
using System.Collections.Generic;
using System.Text;

namespace API.Model
{
    public class WorkTime
    {
        public int WorkTimeID { get; set; }
        public User User { get; set; }
        public DateTime WorkDateTime { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }

        public WorkTime(){}
    }
}
