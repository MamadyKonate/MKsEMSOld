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

        public DbSet<User> Users { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Credentials> Credentials { get; set; } 
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Administraor> Administraors { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; } 
        public DbSet<MKsEMS.Models.UserLogin> UserLogin { get; set; } = default!;
    }
}
