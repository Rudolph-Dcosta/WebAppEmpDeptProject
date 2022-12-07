using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppEmpDeptProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var allEmployee = _unitOfWork.Employee.GetAll();
            return View(allEmployee);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = GetDepartments();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Employee empObj)
        {

            if (ModelState.ErrorCount <=1)
            {
                _unitOfWork.Employee.Add(empObj);
                _unitOfWork.Save();
                TempData["Success"] = "Employee added successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(empObj);
            }
        }
        public IActionResult Edit(int? empId)
        {
            if (empId == null || empId == 0)
            {
                return NotFound();
            }
            ViewBag.Departments = GetDepartments();

            var employeeFirstOrDefault = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == empId);
            return View(employeeFirstOrDefault);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empObj)
        {
            if (ModelState.ErrorCount <= 1)
            {
                _unitOfWork.Employee.Update(empObj);
                _unitOfWork.Save();
                TempData["Success"] = "Employee Edited successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(empObj);
            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employeeFromDb = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);

            if (employeeFromDb == null)
            {
                return NotFound();
            }

            return View(employeeFromDb);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Employee.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Employee Deleted successfully";
            return RedirectToAction(nameof(Index));

        }
        private List<SelectListItem> GetDepartments()
        {
            var lstDepts = new List<SelectListItem>();
            
            var depts =_unitOfWork.Department.GetAll();
            lstDepts =depts.Select(dp => new SelectListItem()
            {
                Value =dp.DeptId.ToString(),
                Text =dp.DeptName
            }).ToList();

            var defDept = new SelectListItem()
            {
                Value = "",
                Text = "---Select Department---"
            };

            lstDepts.Insert(0, defDept);
            return lstDepts;
        }
    }
}
