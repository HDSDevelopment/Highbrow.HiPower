using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Data
{
    public class HiPowerContext : DbContext
    {
        public HiPowerContext(DbContextOptions<HiPowerContext> options)
                : base(options)
        {
        }

        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<WFH> WFHs { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveCategory> LeaveCategories { get; set; }
        public DbSet<LeaveCategoryType> LeaveCategoryTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaveCategoryType>()
            .HasKey(lct => new { lct.LeaveTypeId, lct.LeaveCategoryId });

            modelBuilder.Entity<Employee>()
            .HasOne(n => n.FirstLevelSupervisor)
            .WithMany(n => n.FirstLevelSupervised)
            .HasForeignKey(n => n.FirstLevelSupervisorId);

            modelBuilder.Entity<Employee>()
            .HasOne(n => n.SecondLevelSupervisor)
            .WithMany(n => n.SecondLevelSupervised)
            .HasForeignKey(n => n.SecondLevelSupervisorId);
        }
    }
}