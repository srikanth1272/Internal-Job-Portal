using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models;

public  class JobPost
{
    [Display(Name = "PostId")]

    public int PostId { get; set; }

    [Display(Name = "JobId")]

    public string JobId { get; set; } = null!;

    [Display(Name = "Date Of Posting")]

    public DateOnly DateofPosting { get; set; }
    [Display(Name = "Last Date")]

    public DateOnly LastDatetoApply { get; set; }
    [Display(Name = "Vacancies")]

    [Range(1 ,100, ErrorMessage = "Vacancies must be above zero")]
    public int Vacancies { get; set; }


}
