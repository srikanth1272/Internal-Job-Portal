using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    
    public class Skill
     {
        [Display(Name = "SkillId")]

        [RegularExpression(@"\w{6}", ErrorMessage = "SkillId  must be 6 characters")]

        public string SkillId { get; set; }
        [Display(Name = "Skill Name")]

        public string SkillName { get; set; } = null!;
        // [RegularExpression("^[BIA]", ErrorMessage = "Skill Level must be one character and must be in {B, I, A}")]
        [Display(Name = "Skill Level")]

        public string SkillLevel { get; set; } = null!;
        [Display(Name = "Category")]

        public string SkillCategory { get; set; } = null!;
    }
}
