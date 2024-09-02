using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models;

public  class JobPost
{
    [Display(Name = "Post Id")]

    public int PostId { get; set; }

    [Display(Name = "Job Id")]
    [Required(ErrorMessage = "Job Id is required")]
    public string JobId { get; set; } = null!;


    [Display(Name = "Date Of Posting")]
    [RegularExpression(@"\d{4}-\d{2}-\d{2}", ErrorMessage = "Invalid date format. Use yyyy-MM-dd.")]
    public DateOnly DateofPosting { get; set; }

    [Display(Name = "Last Date")]
    [RegularExpression(@"\d{4}-\d{2}-\d{2}", ErrorMessage = "Invalid date format. Use yyyy-MM-dd.")]
    public DateOnly? LastDatetoApply { get; set; }

    [Display(Name = "Vacancies")]
    [Range(1 ,100, ErrorMessage = "Vacancies must be above zero")]
    public int Vacancies { get; set; }


}
