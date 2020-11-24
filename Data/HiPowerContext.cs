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
        
    public DbSet<CompanyProfile> CompanyProfiles {get; set;}
    public DbSet<Department> Departments {get; set;}
    public DbSet<Designation> Designations { get; set; }
    public DbSet<WFH> WFHs {get; set;}
    public DbSet<Shift> Shifts {get;set;}
}
}