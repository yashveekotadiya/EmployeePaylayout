using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models
{
    public class Employee
    {
         public int c_id { get; set; }
 [Display(Name = "Employee Name")]
          [Required(ErrorMessage = "Employee name is required")]
    [StringLength(100, ErrorMessage = "Employee name must be between {2} and {1} characters", MinimumLength = 2)]

    public string c_employeename { get; set; }

 [Display(Name = "Employee Hire")]
     [Required(ErrorMessage = "Hire date is required")]
    public DateTime c_hiredate { get; set; }

 [Display(Name = "Employee Gender")]
     [Required(ErrorMessage = "Gender is required")]
    public string c_gender { get; set; }

     [Display(Name = "Employee Salary")]
      [Required(ErrorMessage = "Salary is required")]
    public decimal c_salary { get; set; }

 [Display(Name = "Employee Designation")]
     [Required(ErrorMessage = "Designation ID is required")]
    public int c_designationid { get; set; }
    public string c_designation{ get; set; }




    //  public decimal GrossSalary { get; set; }
    public decimal Basic { get; set; }
    public decimal DA { get; set; }
    public decimal HRA { get; set; }
    public decimal Tax { get; set; }
    public decimal Taxable { get; set; }
    public decimal TakeHomePay { get; set; }

    
    }
}