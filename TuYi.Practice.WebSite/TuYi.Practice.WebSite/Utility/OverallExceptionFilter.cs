using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace TuYi.Practice.WebSite.Utility
{
    public class OverallExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.ExceptionHandled = true;
                context.Result = new JsonResult(new
                {
                    Message = context.Exception.Message
                });
            }

            await Task.CompletedTask;
        }
    }
}
