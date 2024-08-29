using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    public class EmployeeSkill
    {

       
        [Display(Name = "Employee Id")]
        [Required(ErrorMessage = "Employee Id Id is required")]
        public string EmpId { get; set; } = null!;

        
        [Display(Name = "Skill Id")]
        [Required(ErrorMessage = "Skill Id is required")]
        public string SkillId { get; set; } = null!;


        [Range(0, 90, ErrorMessage = "Skill Experience must be in between 0 to 90")]
        [Display(Name = "Skill Experience")]

        public int SkillExperience { get; set; }
    }
}
