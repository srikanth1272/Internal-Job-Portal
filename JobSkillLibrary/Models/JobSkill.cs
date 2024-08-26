using System;
using System.Collections.Generic;

namespace JobSkillLibrary.Models;

public partial class JobSkill
{
    public string JobId { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public int Experience { get; set; }
    public virtual Job? Job { get; set; } = null!;
    public virtual Skill? Skill { get; set; } = null!;
}
