using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Model
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }

        public Department()
        {

        }
    }
}
