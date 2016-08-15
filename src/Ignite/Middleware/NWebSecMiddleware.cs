using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ignite.Middleware
{
    public class NWebSecMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NWebSecMiddlewareOptions _middlewareOptions;

        public NWebSecMiddleware(RequestDelegate next, IOptions<NWebSecMiddlewareOptions> options)
        {
            _next = next;
            _middlewareOptions = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.

            await _next.Invoke(context);

            // Clean up.
        }

    }

    public class NWebSecMiddlewareOptions
    {
        public bool ContentSecurityPolicy { get; set; }
        public ContentTypeOptions xContentTypeOptions { get; set; }

        public struct ContentTypeOptions
        {
            public bool Enabled { get; set; }
        }
    }

    public static class NWebSecMiddlewareExtensions
    {
        public static IApplicationBuilder UseNWebSecMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NWebSecMiddleware>();
        }
    }

}


/*
    public class RavenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RavenMiddlewareOptions _ravenMiddlewareOptions;

        public RavenMiddleware(RequestDelegate next, IOptions<RavenMiddlewareOptions> options)
        {
            _next = next;
            _ravenMiddlewareOptions = options.Value;

            RavenDbConfig.Initialize(_ravenMiddlewareOptions.ConnectionString);
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.

            await _next.Invoke(context);

            // Clean up.
        }
    }

    public class RavenMiddlewareOptions
    {
        public string ConnectionString { get; set; }
        //public string Param2 { get; set; }
    }

    public static class RavenMiddlewareExtensions
    {
        public static IApplicationBuilder UseRavenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RavenMiddleware>();
        }
    }


*/
