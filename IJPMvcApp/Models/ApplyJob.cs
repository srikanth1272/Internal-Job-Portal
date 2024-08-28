using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models;

public  class ApplyJob
{
    public int PostId { get; set; }
   
    public string EmpId { get; set; } = null!;
    public DateOnly AppliedDate { get; set ; }
	public string ApplicationStatus { get; set; } = null!;

}
