using System.ComponentModel.DataAnnotations;

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
        public decimal? Salary { get; set; }
        
        [Required(ErrorMessage = "Enter the employee's bonus")]
        [Display(Name = "Bonus")]
        [DataType(DataType.Currency)]
        [Range(0, 10000)]
        public decimal? Bonus { get; set; }

        [Required(ErrorMessage = "Enter the date that this employee started")]
        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }
    }
}
