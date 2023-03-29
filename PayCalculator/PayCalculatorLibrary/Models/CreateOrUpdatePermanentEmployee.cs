using System.ComponentModel.DataAnnotations;

namespace PayCalculatorLibrary.Models
{
    public class CreateOrUpdatePermanentEmployee
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        [Range(0, 100000)]
        public decimal Salary { get; set; }
        
        [Required]
        [Display(Name = "Bonus")]
        [DataType(DataType.Currency)]
        [Range(0, 10000)]
        public decimal Bonus { get; set; }

        [Required]
        [Display(Name = "HoursWorked")]
        [Range(0, 1820)]
        public int HoursWorked { get; set; }
    }
}
