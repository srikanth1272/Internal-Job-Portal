using System;
using System.Collections.Generic;

namespace EmployeeLibrary.Models;

public partial class Employee
{
    public string EmpId { get; set; } = null!;

    public string EmpName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public int TotalExperience { get; set; }

    public string JobId { get; set; } = null!;

    public virtual Job? Job { get; set; } = null!;
}
