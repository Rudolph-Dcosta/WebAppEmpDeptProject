using EmpDeptProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpDeptProject.DataAccess
{
    
    public class EmpDeptDbContext : DbContext
    {
        public EmpDeptDbContext(DbContextOptions<EmpDeptDbContext> options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
