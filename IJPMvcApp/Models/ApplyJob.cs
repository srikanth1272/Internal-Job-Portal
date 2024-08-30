using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models;

public  class ApplyJob
{
    [Display(Name = "Post Id")]
    [Required(ErrorMessage = "Post ID is required")]
    public int PostId { get; set; }


    [Display(Name = "Employee Id")]
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmpId { get; set; } = null!;


    [Display(Name = "Applied Date")]
    public DateOnly AppliedDate { get; set ; }


    [Display(Name = "Application Status")]
    public string ApplicationStatus { get; set; } = null!;

}
