using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; } = "DefaultPassword1";
        public DateTime BirthDay { get; set; }
        public List<Holiday> Holidays { get; set; }
        public List<WorkTime> WorkTimes { get; set; } 
        public Contract Contract { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }

        public User()
        {
        }

        public bool HasContract()
        {
            return Contract != null;
        }

        public bool IsManagerOfDepartment()
        {
            return Department.Manager == this;
        }

        public int NumberOfHolidaysLeft()
        {
            var returnValue = 0;

            foreach (var holiday in Holidays)
            {
                returnValue += (int)holiday.NumberOfWorkDays();
            }

            return returnValue;
        }

    }
}
