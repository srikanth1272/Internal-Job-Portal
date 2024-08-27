using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models;

public  class JobPost
{
    public int PostId { get; set; }

    
    public string JobId { get; set; } = null!;

    public DateOnly DateofPosting { get; set; }

    public DateOnly LastDatetoApply { get; set; }
    [Range(1 ,100, ErrorMessage = "Vacancies must be above zero")]
    public int Vacancies { get; set; }


}
