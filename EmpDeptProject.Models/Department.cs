using System.ComponentModel.DataAnnotations;

namespace EmpDeptProject.Models
{
    public class Department
    {
        [Key]
        [Display(Name = "Department Id")]

        public int DeptId { get; set; }
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Department Name should contain only Characters")]

        [Required]
        [Display(Name = "Department Name")]

        public string DeptName { get; set; }
        //public ICollection<Employee> Employee { get; set; }


    }
}
