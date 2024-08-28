using System;
using System.Collections.Generic;

namespace ApplyJobLibrary.Models;

public partial class ApplyJob
{
    public int PostId { get; set; }

    public string EmpId { get; set; } = null!;

    public DateOnly AppliedDate { get; set; }

    public string ApplicationStatus { get; set; } = null!;

    public virtual Employee? Emp { get; set; } = null!;

    public virtual JobPost? Post { get; set; } = null!;
}
