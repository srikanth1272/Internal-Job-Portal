using System;
using System.Collections.Generic;

namespace EmployeeSkillLibrary.Models;

public partial class EmployeeSkill
{
    public string EmpId { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public int SkillExperience { get; set; }

    public virtual Employee? Emp { get; set; } = null!;

    public virtual Skill? Skill { get; set; } = null!;
}
