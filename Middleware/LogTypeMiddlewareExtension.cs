using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autolog.Middleware
{
    public static class LogTypeMiddlewareExtension
    {
        public static IApplicationBuilder UseLogTypeBuilder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogTypeMiddleware>();
        }
    }
}