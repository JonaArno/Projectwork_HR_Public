using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Links;
using API.DTO.WorkTime;
using API.Model;

namespace API.DomainServices.Interfaces
{
    public interface IWorkTimeService
    {
        IEnumerable<WorkTimeReturnDto> GetAllWorkTimes();
        WorkTimeReturnDto GetWorkTimeById(int id);
        IEnumerable<WorkTimeReturnDto> GetWorkTimesForTimePeriod(DateTime startDateTime, DateTime endDateTime);
        Tuple<WorkTimeReturnMinifiedDto, LinkDto> RegisterWorkTime(int userId);
        WorkTimeReturnDto CorrectWorkTime(int workTimeId, UpdateWorkTimeDto updatedWorkTime);
        void DeleteWorkTime(int workTimeId);
    }
}
