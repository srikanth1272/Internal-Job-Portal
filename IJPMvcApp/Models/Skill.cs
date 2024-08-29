using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    
    public class Skill
     {
        [Display(Name = "Skill Id")]
        [RegularExpression(@"\w{6}", ErrorMessage = "SkillId  must be 6 characters")]
        public string SkillId { get; set; }
        
        [Display(Name = "Skill Name")]
        public string SkillName { get; set; } = null!;
       
        [Display(Name = "Skill Level")]
        [Required(ErrorMessage = "Skill Level is required")]
        public string SkillLevel { get; set; } = null!;
        
        [Display(Name = "Category")]
        public string SkillCategory { get; set; } = null!;
    }
}
