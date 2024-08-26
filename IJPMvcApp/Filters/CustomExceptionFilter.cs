using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace IJPMvcApp.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {

        private readonly IModelMetadataProvider _modelMetadataProvider;
        public CustomExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }
        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "Error" };
            var viewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            {
                { "ErrorMessage", context.Exception.Message },
                { "Exception", context.Exception }
            };

            result.ViewData = viewData;
            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}
