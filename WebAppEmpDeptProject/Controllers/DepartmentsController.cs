using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppEmpDeptProject.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var allDepartment = _unitOfWork.Department.GetAll();
            return View(allDepartment);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult Create(Department deptObj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Add(deptObj);
                _unitOfWork.Save();
                TempData["Success"] = "Department added successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(deptObj);
            }
        }
        public IActionResult Edit(int ? deptId)
        {
            if (deptId == null || deptId == 0)
            {
                return NotFound();
            }
            var departmentFirstOrDefault = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == deptId);
            return View(departmentFirstOrDefault);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department deptObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Update(deptObj);
                _unitOfWork.Save();
                TempData["Success"] = "Department Edited successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(deptObj);
            }
        }
        public IActionResult Delete(int ? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var departmentFromDb = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
           
            if (departmentFromDb == null)
            {
                return NotFound();
            }

            return View(departmentFromDb);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int ? id)
        {
            var obj = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Department.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Department Deleted successfully";
            return RedirectToAction(nameof(Index));

        }
    }
}
