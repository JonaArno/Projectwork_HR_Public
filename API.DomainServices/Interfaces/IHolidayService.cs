using System;
using System.Collections.Generic;
using System.Text;
using API.DTO;
using API.DTO.Holiday;
using API.DTO.Links;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.DomainServices.Interfaces
{
    public interface IHolidayService
    {
        IEnumerable<HolidayReturnDto> GetAllHolidays();
        IEnumerable<HolidayReturnDto> GetAllHolidaysForYear(int year);
        HolidayReturnDto GetHolidayById(int id);
        Tuple<HolidayCreatedReturnDto, LinkDto> RequestHoliday(int userId, DateTime startDateTime, DateTime endDateTime);
        HolidayReturnDto UpdateHoliday(int id, UpdateHolidayDto updatedHoliday);
        void DeleteHoliday(int id);
        IEnumerable<HolidayReturnDto> GetHolidaysToApprove(int managerId);
        HolidayReturnDto ApproveHoliday(int holidayId, int managerId);
    }
}
