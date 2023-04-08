using Microsoft.EntityFrameworkCore;
using MKsEMS.Models;

namespace MKsEMS.Data
{
    public class EMSDbContext : DbContext
    {
        //public DbSet<User> Users { get; set; }
        //public DbSet<Manager> Managers { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Credentials> Credentials { get; set; }
        //public DbSet<Contact> Contacts { get; set; }
        //public DbSet<Leave> Leaves { get; set; }
        //public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\TempDB\EMSDbContext.db");

        public DbSet<MKsEMS.Models.User> Users { get; set; } = default!;
        public DbSet<MKsEMS.Models.Manager> Managers { get; set; } = default!;
        public DbSet<MKsEMS.Models.Employee> Employees { get; set; } = default!;
        public DbSet<MKsEMS.Models.Credentials> Credentials { get; set; } = default!;
        public DbSet<MKsEMS.Models.Contact> Contacts { get; set; } = default!;
        public DbSet<MKsEMS.Models.Leave> Leaves { get; set; } = default!;
        public DbSet<MKsEMS.Models.LeaveType> LeaveTypes { get; set; } = default!; 
    }
}
