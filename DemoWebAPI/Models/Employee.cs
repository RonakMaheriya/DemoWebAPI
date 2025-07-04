using System.ComponentModel.DataAnnotations;
namespace DemoWebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Age is required")]
        [Range(18,60, ErrorMessage ="Age should be between 18 to 60")]
        public int Age { get; set; }
        [Required(ErrorMessage ="Designation is required")]
        public string Designation { get; set; }
        public string Salary { get; set; }
    }

}
