using System;
using System.Collections.Generic;

namespace JobSkillLibrary.Models;

public partial class Job
{
    public string JobId { get; set; } = null!;

    public virtual ICollection<JobSkill>? JobSkills { get; set; } = new List<JobSkill>();
}
