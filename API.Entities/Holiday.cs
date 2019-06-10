using System;
using System.Collections.Generic;

namespace API.Model
{
    public class Holiday
    {
        public int ID { get; set; }
        public User User { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }

        public Holiday()
        { }

        public int NumberOfWorkDays()
        {
            var weekDays = new List<DateTime>();

            var iterationDateTime = StartDateTime;

            while (iterationDateTime <= EndDateTime)
            {
                if (iterationDateTime.DayOfWeek != DayOfWeek.Saturday &&
                    iterationDateTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekDays.Add(iterationDateTime);
                }

                iterationDateTime = iterationDateTime.AddDays(1);
            }

            var returnValue = weekDays.Count;
            return returnValue;
        }
    }
}
