using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace PayCalculatorLibrary.Models
{
    public class CreateOrUpdatePermanentEmployee
    {
        [Required(ErrorMessage = "Enter the employee's name")]
        [Display(Name = "Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the employee's salary")]
        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        [Range(0, 100000)]
        public decimal Salary { get; set; }
        
        [Required(ErrorMessage = "Enter the employee's bonus")]
        [Display(Name = "Bonus")]
        [DataType(DataType.Currency)]
        [Range(0, 10000)]
        public decimal Bonus { get; set; }

        [Required(ErrorMessage = "Enter the number of hours this employee has worked")]
        [Display(Name = "HoursWorked")]
        [Range(0, 1820)]
        public int HoursWorked { get; set; }
    }
}
