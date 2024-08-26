using System;
using System.Collections.Generic;

namespace EmployeeSkillLibrary.Models;

public partial class Skill
{
    public string SkillId { get; set; } = null!;

    public virtual ICollection<EmployeeSkill>? EmployeeSkills { get; set; } = new List<EmployeeSkill>();
}
