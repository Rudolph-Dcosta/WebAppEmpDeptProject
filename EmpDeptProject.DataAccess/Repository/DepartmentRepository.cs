using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models;


namespace EmpDeptProject.DataAccess.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly EmpDeptDbContext _db;

        public DepartmentRepository(EmpDeptDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Department obj)
        {
            _db.Update(obj);
        }
    }
}
