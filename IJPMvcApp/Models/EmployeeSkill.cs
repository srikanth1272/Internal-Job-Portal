using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    public class EmployeeSkill
    {

        [RegularExpression(@"\w{6}", ErrorMessage = "Employee Id must be 6")]
        [Display(Name = "Employee Id")]
        public string EmpId { get; set; } = null!;

        [RegularExpression(@"\w{6}", ErrorMessage = "Skill Id must be 6")]
        [Display(Name = "Skill Id")]
        public string SkillId { get; set; } = null!;
        [Range(1, 90, ErrorMessage = "Skill Experience must be in between 0 to 90")]
        public int SkillExperience { get; set; }
    }
}
