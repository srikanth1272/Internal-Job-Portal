using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    public partial class JobSkill
    {
        
        [Display(Name = "Job Id")]
        [Required(ErrorMessage = "Job Id is required")]
        public string JobId { get; set; } = null!;
        
        [Display(Name = "Skill Id")]
        [Required(ErrorMessage = "Skill Id is required")]
        public string SkillId { get; set; } = null!;

        [Display(Name = "Experience")]
        [Range(0, 50, ErrorMessage = "Experience must be 0 to 50")]
        public int Experience { get; set; }
    }
}
