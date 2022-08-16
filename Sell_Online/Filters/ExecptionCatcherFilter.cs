using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sell_Online.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Filters
{
    public class ExecptionCatcherFilter : ExceptionFilterAttribute, IAsyncExceptionFilter
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var result = new JsonResult(new GeneralResponse
            {
                ErrorCode = Guid.NewGuid().ToString(),
                Message = context.Exception.Message,
            });

            // logging

            result.StatusCode = 500;
            context.Result = result;
            return Task.CompletedTask;
        }
    }
}
