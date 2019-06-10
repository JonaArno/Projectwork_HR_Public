using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Model
{
    public class Contract
    {
        public int ContractID { get; set; }
        public decimal GrossSalary { get; set; }
        public int NumberOfHolidays { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }

        [ForeignKey("EmployeeId")]
        public User User { get; set; }

        public Contract()
        {
        }

        public bool HasAssociatedUser()
        {
            return this.User != null;
        }
    }
}
