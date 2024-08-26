using System;
using System.Collections.Generic;

namespace JobLibrary.Models;

public partial class Job
{
    public string JobId { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public string JobDescription { get; set; } = null!;

    public decimal Salary { get; set; }
}
