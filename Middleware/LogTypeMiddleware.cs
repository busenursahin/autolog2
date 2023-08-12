using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autolog.Helper;

namespace autolog.Middleware
{
    public class LogTypeMiddleware
    {
        private IConfiguration _configuration;

        private readonly RequestDelegate _next;

        public LogTypeMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            DbHelper dbHelper = new DbHelper(_configuration);
            dbHelper.CreateLogManagerClassFromDb();

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }
}