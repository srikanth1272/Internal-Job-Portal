using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    public partial class JobSkill
    {
        [RegularExpression(@"\w{6}", ErrorMessage = "Course code must be 6 characters")]
        [Display(Name = "Job Id")]
        public string JobId { get; set; } = null!;
        [RegularExpression(@"\w{6}", ErrorMessage = "Course code must be 6 characters")]
        [Display(Name = "Skill Id")]
        public string SkillId { get; set; } = null!;
        [Display(Name = "Experience")]

        [Range(0, 50, ErrorMessage = "Experience must be 0 to 50")]
        public int Experience { get; set; }
    }
}
