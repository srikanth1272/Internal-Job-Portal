using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    
    public class Skill
    {
        [RegularExpression(@"\w{6}", ErrorMessage = "SkillId  must be 6 characters")]

        public string SkillId { get; set; }
        public string SkillName { get; set; } = null!;
       // [RegularExpression("^[BIA]", ErrorMessage = "Skill Level must be one character and must be in {B, I, A}")]

        public string SkillLevel { get; set; } = null!;

        public string SkillCategory { get; set; } = null!;
    }
}
