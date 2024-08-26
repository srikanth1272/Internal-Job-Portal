using System;
using System.Collections.Generic;

namespace JobSkillLibrary.Models;

public partial class Skill
{
    public string SkillId { get; set; } = null!;

    public virtual ICollection<JobSkill>? JobSkills { get; set; } = new List<JobSkill>();
}
