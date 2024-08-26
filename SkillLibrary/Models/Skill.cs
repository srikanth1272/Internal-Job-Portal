using System;
using System.Collections.Generic;

namespace SkillLibrary.Models;

public partial class Skill
{
    public string SkillId { get; set; } = null!;

    public string SkillName { get; set; } = null!;

    public string SkillLevel { get; set; } = null!;

    public string SkillCategory { get; set; } = null!;
}
