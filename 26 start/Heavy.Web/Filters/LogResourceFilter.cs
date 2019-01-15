using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Heavy.Web.Filters
{
    public class LogResourceFilter: Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Executing Resource Filter!");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.Write("Executed Resource Filter...");
        }
    }
}
