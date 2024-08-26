using System;
using System.Collections.Generic;

namespace JobPostLibrary.Models;

public partial class JobPost
{
    public int PostId { get; set; }

    public string JobId { get; set; } = null!;

    public DateOnly DateofPosting { get; set; }

    public DateOnly LastDatetoApply { get; set; }

    public int Vacancies { get; set; }

  
}
