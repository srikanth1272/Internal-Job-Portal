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
        // [MaxLength(10, ErrorMessage = "Phone No must be 10 digits")]
       // [Phone]
        [RegularExpression(@"\d{10}", ErrorMessage = "phone number must be 10 digits")]
        public string PhoneNo { get; set; } = null!;
        [Range(0,90,ErrorMessage ="Experience must be in between 0 to 90")]
        public int TotalExperience { get; set; }
        [RegularExpression(@"\w{6}", ErrorMessage = "Job Id must be 6 characters")]
        [Display(Name = "Job Id")]
        public string JobId { get; set; } = null!;
    }
}
