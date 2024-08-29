using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    public class Employee
    {
        [RegularExpression(@"\w{6}", ErrorMessage ="Employee Id must be 6")]
        [Display(Name="Employee Id")]
        public string EmpId { get; set; } = null!;

        [MaxLength(30, ErrorMessage ="Name cannot be more than 30 characters")]
        [Display(Name ="Employee Name")]
        public string EmpName { get; set; } = null!;
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Incorrect Email Format")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; } = null!;
       
        [RegularExpression(@"\d{10}", ErrorMessage = "phone number must be 10 digits")]
        public string PhoneNo { get; set; } = null!;


        [Range(0,90,ErrorMessage ="Experience must be in between 0 to 90")]
        [Display(Name = "Total Experience")]
        public int TotalExperience { get; set; }
       
        [Display(Name = "Job Id")]
        [Required(ErrorMessage ="Job Id is required")]
        public string JobId { get; set; } = null!;
    }
}
