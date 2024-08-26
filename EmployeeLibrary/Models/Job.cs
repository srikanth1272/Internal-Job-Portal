using System;
using System.Collections.Generic;

namespace EmployeeLibrary.Models;

public partial class Job
{
    public string JobId { get; set; } = null!;

    public virtual ICollection<Employee>? Employees { get; set; } = new List<Employee>();
}
