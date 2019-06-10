using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class ClearTables
    {
        public static void Execute(HrApplicationContext context)
        {
            ClearDepartmentManagers(context);
            context.Users.Clear();
            context.Departments.Clear();
            context.Contracts.Clear();
            context.Holidays.Clear();
            context.Worktimes.Clear();
            context.SaveChanges();
        }

        private static void ClearDepartmentManagers(HrApplicationContext context)
        {
            var departments = context.Departments
                .Include(dep => dep.Manager)
                .ToList();
            foreach (var dep in departments)
            {
                dep.Manager = null;
            }
            context.UpdateRange(departments);
            context.SaveChanges();
        }
    }
}
