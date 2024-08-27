using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{

    
    public class Job
    {
        [RegularExpression(@"\w{6}", ErrorMessage = "JobId must be 6 characters")]
        public string JobId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobDescription { get; set; } = null!;

        public decimal Salary { get; set; }
    }
}
