using CubeService.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CubeService.Middleware
{
    public class ActionErrorHandlingFilter : IAlwaysRunResultFilter, IExceptionFilter
    {
        private SharedError Error { get; set; }

        public ActionErrorHandlingFilter(SharedError error)
        {
            Error = error;
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result != null)
            {
                // If there is an error, output it
                if (Error.Message != null)
                {
                    context.HttpContext.Response.ContentType = "Text/Plain";
                    if (Error.StatusCode != null && context.HttpContext.Response.StatusCode != Error.StatusCode)
                    {
                        context.HttpContext.Response.StatusCode = (int)Error.StatusCode;
                    }
                    context.HttpContext.Response.WriteAsync(Error.Message);
                }
                else if ((context.HttpContext.Response.StatusCode >= 400 && context.HttpContext.Response.StatusCode < 600) || (Error.StatusCode >= 400 && Error.StatusCode < 600))
                {
                    if (Error.StatusCode != null && context.HttpContext.Response.StatusCode != Error.StatusCode)
                    {
                        context.HttpContext.Response.StatusCode = (int)Error.StatusCode;
                    }
                    context.HttpContext.Response.ContentType = "Text/Plain";
                    context.HttpContext.Response.WriteAsync("An unexpected error occurred");
                }

            }
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            OnResultExecuting(context);
            await next();
        }
        public void OnResultExecuted(ResultExecutedContext context) { }
        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "Text/Plain";
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.WriteAsync("An exception occurred in " + context.ActionDescriptor.DisplayName + ": (" + context.Exception.Message + ")");
        }
    }
}
