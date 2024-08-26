using System;
using System.Collections.Generic;

namespace EmployeeSkillLibrary.Models;

public partial class Employee
{
    public string EmpId { get; set; } = null!;

    public virtual ICollection<EmployeeSkill>? EmployeeSkills { get; set; } = new List<EmployeeSkill>();
}
