using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
   

        public class Job
     {
        [RegularExpression(@"\w{6}", ErrorMessage = "JobId must be 6 characters")]
        [Display(Name ="JobId")]
        public string JobId { get; set; }
        [Display(Name ="Job Title")]
        public string JobTitle { get; set; } = null!;
        [Display(Name = "Description")]

        public string JobDescription { get; set; } = null!;
        [Range(10000,1000000000,ErrorMessage ="Salary must be greater than 10000")]
        [Display(Name = "Salary")]

        public decimal Salary { get; set; }
    }
}
