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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.DomainOperations
{
    public class HolidayOperations : IHolidayOperations
    {
        private readonly HrApplicationContext _context;

        public HolidayOperations(HrApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Holiday> GetAllHolidays()
        {
            return _context.Holidays
                .OrderBy(h => h.StartDateTime)
                .ToList();
        }

        public IEnumerable<Holiday> GetAllHolidaysForYear(int year)
        {
            return _context.Holidays.Where(
                hol => hol.StartDateTime.Year == year || hol.EndDateTime.Year == year)
                .OrderBy(h => h.StartDateTime)
                .ToList();
        }

        public IEnumerable<Holiday> GetAllHolidaysForUserForYear(User user, int year)
        {
            return _context.Holidays
                    .Where(hol => hol.User == user)
                    .Where(hol => hol.StartDateTime.Year == year || hol.EndDateTime.Year == year)
                    .OrderBy(h => h.StartDateTime)
                    .ToList();
        }

        public Holiday GetHolidayById(int id)
        {
            return _context.Holidays
                .Include(hol => hol.User)
                .First(hol => hol.ID == id);
        }

        public Holiday RequestHoliday(User user, DateTime startDateTime, DateTime endDateTime, bool isManager)
        {
            if (!DatesValid(startDateTime, endDateTime)) return null;
            var numberOfWorkDays = SpansMultipleYears(startDateTime, endDateTime) ? NumberOfWorkDaysPerYear(startDateTime, endDateTime) : CalculateNumberOfWorkDaysForSameYear(startDateTime, endDateTime);
            if (!UserHasEnoughHolidaysLeft(user, numberOfWorkDays)) return null;
            var newHoliday = new Holiday
            {
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                User = user,
                CreationDate = DateTime.Now,
                LastModified = DateTime.Now
            };
            if (isManager) newHoliday.IsApproved = true;
            _context.Holidays.Add(newHoliday);
            if (!Save()) throw new Exception($"Error while persisting new holiday for user {user.UserID} to the database.");
            return newHoliday;
        }

        private Dictionary<int, int> CalculateNumberOfWorkDaysForSameYear(DateTime startDateTime, DateTime endDateTime)
        {
            var workDaysCollection = new Dictionary<int, int>();
            var iterationDateTime = startDateTime;
            var counter = 0;

            while (iterationDateTime <= endDateTime)
            {
                if (iterationDateTime.DayOfWeek != DayOfWeek.Saturday &&
                    iterationDateTime.DayOfWeek != DayOfWeek.Sunday) counter += 1;
                if (iterationDateTime == endDateTime)
                {
                    workDaysCollection.Add(iterationDateTime.Year, counter);
                    break;
                }
                iterationDateTime = iterationDateTime.AddDays(1);
            }
            return workDaysCollection;
        }

        private bool SpansMultipleYears(DateTime startDateTime, DateTime endDateTime)
        {
            return startDateTime.Year != endDateTime.Year;
        }

        public IEnumerable<Holiday> GetUnapprovedHolidays(IEnumerable<User> users)
        {
            return users.SelectMany(user => _context.Holidays.Where(
                        hol => hol.User == user && hol.IsApproved == false)
                .OrderBy(h => h.StartDateTime)
                .ToList());
        }

        public Holiday UpdateHoliday(Holiday holiday, DateTime newStartDate, DateTime newEndDate, bool isManager)
        {
            holiday.StartDateTime = newStartDate;
            holiday.EndDateTime = newEndDate;

            if (!isManager) holiday.IsApproved = false;
            else holiday.IsApproved = true;
            holiday.LastModified = DateTime.Now;
            _context.Holidays.Update(holiday);
            if (!Save()) throw new Exception($"Problem while updating holiday with id {holiday.ID}.");
            return holiday;
        }

        public void RemoveHoliday(Holiday holiday)
        {
            _context.Holidays.Remove(holiday);
            if (!Save()) throw new Exception($"Problem removing holiday with id {holiday.ID}.");
        }

        public bool HolidayExists(int id)
        {
            return _context.Holidays.Any(hol => hol.ID == id);
        }

        public Holiday ApproveHoliday(Holiday holiday)
        {
            holiday.IsApproved = true;
            _context.Holidays.Update(holiday);
            if (!Save()) throw new Exception($"Problem while approving holiday with id {holiday.ID}.");
            return holiday;
        }

        private bool UserHasEnoughHolidaysLeft(User user, Dictionary<int, int> numberOfWorkDaysInRequestedPeriod)
        {
            var userHolidaysPerYear = user.Contract.NumberOfHolidays;
            var existingHolidays = new Dictionary<int, int>();

            foreach (var entry in numberOfWorkDaysInRequestedPeriod)
            {
                var holidaysLinkedToKey = _context.Holidays.Where(hol =>
                    hol.User == user && (hol.StartDateTime.Year == entry.Key ||
                                         hol.EndDateTime.Year == entry.Key))
                    .ToList();

                existingHolidays.Add(entry.Key, NumberOfWorkDaysInYearForHolidayCollection(holidaysLinkedToKey, entry.Key));
            }

            foreach (var pair in numberOfWorkDaysInRequestedPeriod)
            {
                var totalDays = pair.Value + existingHolidays[pair.Key];
                if (totalDays > userHolidaysPerYear) return false;
            }
            return true;
        }

        private int NumberOfWorkDaysInYearForHolidayCollection(IEnumerable<Holiday> holidaysLinkedToKey, int entryKey)
        {
            var returnValue = 0;

            foreach (var holiday in holidaysLinkedToKey)
            {
                if (!SpansMultipleYears(holiday.StartDateTime, holiday.EndDateTime))
                {
                    returnValue += NumberOfWorkDays(holiday);
                }
                else
                {
                    returnValue += NumberOfWorkDaysInGivenYear(holiday, entryKey);
                }
            }

            return returnValue;
        }

        private int NumberOfWorkDaysInGivenYear(Holiday holiday, int entryKey)
        {
            var iterationDate = holiday.StartDateTime;
            var counter = 0;

            if (holiday.StartDateTime.Year == entryKey)
            {
                var lastDayOfYear = new DateTime(entryKey, 12, 31);
                while (iterationDate <= lastDayOfYear)
                {
                    if (iterationDate.DayOfWeek != DayOfWeek.Saturday && iterationDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        counter += 1;
                    }
                    iterationDate = iterationDate.AddDays(1);
                }
            }

            else
            {
                while (iterationDate <= holiday.EndDateTime)
                {
                    if (iterationDate.DayOfWeek != DayOfWeek.Saturday && iterationDate.DayOfWeek != DayOfWeek.Sunday && iterationDate.Year == entryKey)
                    {
                        counter += 1;
                    }
                    iterationDate = iterationDate.AddDays(1);
                }
            }
            return counter;
        }
        
        private int NumberOfWorkDays(Holiday holiday)
        {
            var weekDays = new List<DateTime>();
            var iterationDateTime = holiday.StartDateTime;

            while (iterationDateTime <= holiday.EndDateTime)
            {
                if (iterationDateTime.DayOfWeek != DayOfWeek.Saturday &&
                    iterationDateTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekDays.Add(iterationDateTime);
                }
                iterationDateTime = iterationDateTime.AddDays(1);
            }
            return weekDays.Count;
        }

        private Dictionary<int, int> NumberOfWorkDaysPerYear(DateTime startDateTime, DateTime endDateTime)
        {
            var workDaysCollection = new Dictionary<int, int>();
            var iterationDateTime = startDateTime;
            var counter = 0;

            while (iterationDateTime <= endDateTime)
            {
                if (iterationDateTime.DayOfWeek != DayOfWeek.Saturday &&
                    iterationDateTime.DayOfWeek != DayOfWeek.Sunday) counter += 1;
                if (IsLastDayOfYear(iterationDateTime) || iterationDateTime == endDateTime)
                {
                    workDaysCollection.Add(iterationDateTime.Year, counter);
                    counter = 0;
                }
                iterationDateTime = iterationDateTime.AddDays(1);
            }
            return workDaysCollection;
        }

        private bool IsLastDayOfYear(DateTime date)
        {
            return date.Month == 12 && date.Day == 31;
        }

        private bool DatesValid(DateTime startDateTime, DateTime endDateTime)
        {
            return startDateTime >= DateTime.Today && endDateTime >= startDateTime;
        }

        private bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
