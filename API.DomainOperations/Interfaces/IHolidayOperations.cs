using System;
using System.Collections.Generic;
using System.Text;
using API.DTO.Links;
using API.Model;

namespace API.DomainOperations.Interfaces
{
    public interface IHolidayOperations
    {
        IEnumerable<Holiday> GetAllHolidays();
        IEnumerable<Holiday> GetAllHolidaysForYear(int year);
        IEnumerable<Holiday> GetAllHolidaysForUserForYear(User user, int year);
        Holiday GetHolidayById(int id);
        Holiday RequestHoliday(User user, DateTime startDateTime, DateTime endDateTime, bool isManager);
        IEnumerable<Holiday> GetUnapprovedHolidays(IEnumerable<User> users);
        Holiday UpdateHoliday(Holiday holiday, DateTime newStartDate, DateTime newEndDate, bool isManager);
        void RemoveHoliday(Holiday holiday);
        bool HolidayExists(int id);
        Holiday ApproveHoliday(Holiday holiday);
    }
}
