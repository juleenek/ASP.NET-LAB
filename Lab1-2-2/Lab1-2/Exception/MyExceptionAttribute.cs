using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Exception
{
    public class MyExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var body = new Dictionary<string, Object>();
            body["error"] = context.Exception.Message;
            context.Result = new BadRequestObjectResult(body);
        }
    }

}
