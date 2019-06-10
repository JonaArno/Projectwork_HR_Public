using System;
using Microsoft.EntityFrameworkCore;
using API.Model;

namespace API.Data
{
    public class HrApplicationContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkTime> Worktimes { get; set; }

        public HrApplicationContext(DbContextOptions<HrApplicationContext> options) : base(options)
        {
        }
    }
}
