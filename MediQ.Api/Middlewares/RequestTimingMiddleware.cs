using NLog;
using System.Diagnostics;

namespace MediQ.Api.Middlewares
{
    public class RequestTimingMiddleware : IMiddleware
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next(context);
            stopwatch.Stop();
            //Console.WriteLine($"Request took: {stopwatch.ElapsedMilliseconds} ms");
            _logger.Info($"Request took: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
