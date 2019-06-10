using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Data;
using API.DomainOperations.Interfaces;
using API.DTO.Links;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.DomainOperations
{
    public class WorkTimeOperations : IWorkTimeOperations
    {
        private readonly HrApplicationContext _context;

        public WorkTimeOperations(HrApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<WorkTime> GetAllWorkTimes()
        {
            return _context.Worktimes
                .OrderBy(wt => wt.WorkDateTime)
                .Include(wt => wt.User)
                .ToList();
        }

        public IEnumerable<WorkTime> GetWorkTimeOverViewForUserForPeriod(User user, DateTime startDateTime, DateTime endDateTime)
        {
            return _context.Worktimes.Where(
                    wt => wt.WorkDateTime >= startDateTime && wt.WorkDateTime <= endDateTime && wt.User == user)
                .OrderBy(wt => wt.WorkDateTime)
                .ToList();
        }

        public IEnumerable<WorkTime> GetWorkTimesForTimePeriod(DateTime startDateTime, DateTime endDateTime)
        {
            return _context.Worktimes.Where(wt => wt.WorkDateTime >= startDateTime && wt.WorkDateTime <= endDateTime)
                .Include(wt => wt.User)
                .OrderBy(wt => wt.WorkDateTime)
                .ToList();
        }

        public WorkTime GetWorkTimeById(int id)
        {
            return _context.Worktimes
                .Include(wt => wt.User)
                .First(wt => wt.WorkTimeID == id);
        }

        public WorkTime RegisterWorkTime(User user)
        {
            var workTime = new WorkTime
            {
                User = user,
                WorkDateTime = DateTime.Now,
                CreationDate = DateTime.Now,
                LastModified = DateTime.Now
            };
            _context.Worktimes.Add(workTime);
            if (!Save()) throw new Exception($"Issue while registering work time for user with id {user.UserID}. ");
            return workTime;
        }

        public WorkTime CorrectWorkTime(WorkTime workTime, DateTime newDateTime)
        {
            workTime.WorkDateTime = newDateTime;
            workTime.LastModified = DateTime.Now;
            _context.Worktimes.Update(workTime);
            if (!Save()) throw new Exception($"Problem while persisting change to work time with id {workTime.WorkTimeID} to database.");
            return workTime;
        }

        public void DeleteWorkTime(WorkTime workTimeToRemove)
        {
            _context.Worktimes.Remove(workTimeToRemove);
            if (!Save()) throw new Exception($"Problem why removing work time registration with id {workTimeToRemove.WorkTimeID} from the database.");
        }

        public bool IsValidWorkTime(int workTimeId)
        {
            return _context.Worktimes.Any(wt => wt.WorkTimeID == workTimeId);
        }
        
        private bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}
