using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models;
using Microsoft.EntityFrameworkCore;


namespace EmpDeptProject.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EmpDeptDbContext _db;

        public EmployeeRepository(EmpDeptDbContext db) : base(db)
        {
            _db = db;
        }
        
        override public IEnumerable<Employee> GetAll() 
        {
            IQueryable<Employee> query = dbset.Include(d=>d.Departments); //Eager Loading
            return query.ToList();
        }

        public void Update(Employee obj)
        {
            _db.Update(obj);
        }
    }
}
