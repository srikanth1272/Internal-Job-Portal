using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace IJPMvcApp.Filters
{
    public class LogActionFilterAttribute:ActionFilterAttribute
    {
        
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                RouteData rData = context.RouteData;
                var controller = rData.Values["controller"];
                var action = rData.Values["action"];
                Debug.WriteLine($"The {action} action in {controller} controller is executing");
            }
        
    }
}
