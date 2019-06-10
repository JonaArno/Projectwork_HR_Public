using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Links;
using API.Model;

namespace API.DomainOperations.Interfaces
{
    public interface IWorkTimeOperations
    {
        IEnumerable<WorkTime> GetAllWorkTimes();
        IEnumerable<WorkTime> GetWorkTimeOverViewForUserForPeriod(User user, DateTime startDateTime,
            DateTime endDateTime);
        IEnumerable<WorkTime> GetWorkTimesForTimePeriod(DateTime startDateTime, DateTime endDateTime);
        WorkTime GetWorkTimeById(int id);
        WorkTime RegisterWorkTime(User user);
        WorkTime CorrectWorkTime(WorkTime workTime, DateTime newDateTime);
        void DeleteWorkTime(WorkTime workTime);
        bool IsValidWorkTime(int workTimeId);
    }
}
