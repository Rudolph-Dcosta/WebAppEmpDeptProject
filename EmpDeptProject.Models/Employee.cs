

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpDeptProject.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Employee Id")]

        public int EmpId { get; set; }
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Employee Name should contain only Characters")]

        [Required]
        [Display(Name = "Employee Name")]

        public string EmpName { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Employee Salary should contain only Numbers")]
        [Display(Name = "Employee Salary")]

        public double EmpSal { get; set; }
        [Required]
        [ForeignKey("Departments")]
        [Display(Name = "Department Id")]


        public int DeptId { get; set; }
        public virtual Department Departments { get; set; }

    }
}
