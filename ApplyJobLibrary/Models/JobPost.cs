using System;
using System.Collections.Generic;

namespace ApplyJobLibrary.Models;

public partial class JobPost
{
    public int PostId { get; set; }
    public DateOnly LastDatetoApply {  get; set; }  // forchecking applied date is within lastdate to apply

    public virtual ICollection<ApplyJob>? ApplyJobs { get; set; } = new List<ApplyJob>();
}
