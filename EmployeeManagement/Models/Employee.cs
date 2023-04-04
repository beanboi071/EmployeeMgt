using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]+(?: [A-Za-z]+)*$", ErrorMessage = "Invalid Input!")]
        [MaxLength(50,ErrorMessage ="Name can't be more than 50 characters.")]
        public string? Name { get; set; }
        [Required]
        [Display(Name="Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid email format")]
        public string? Email { get; set; }
        
        public Dept? Department { get; set; }
        public string? Photo { get; set; }
    }
    public enum Dept
    {
        abc,
        xyz,
        rst,
        mno
    }
}
