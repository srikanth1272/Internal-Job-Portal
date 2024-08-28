using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models;

public  class ApplyJob
{
    [Display(Name = "PostId")]

    public int PostId { get; set; }
    [Display(Name = "Employee Id")]

    public string EmpId { get; set; } = null!;
    [Display(Name = "Applied Date")]

    public DateOnly AppliedDate { get; set ; }
    [Display(Name = "Application Status")]

    public string ApplicationStatus { get; set; } = null!;

}
