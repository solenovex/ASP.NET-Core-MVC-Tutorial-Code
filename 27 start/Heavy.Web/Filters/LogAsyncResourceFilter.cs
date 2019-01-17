using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Heavy.Web.Filters
{
    public class LogAsyncResourceFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(
            ResourceExecutingContext context, 
            ResourceExecutionDelegate next)
        {
            Console.WriteLine("Executing async Resource Filter!");
            var executedContext = await next();
            Console.WriteLine("Executed async Resource Filter...");
        }
    }
}
