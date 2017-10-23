using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace TestConsumer.Framework
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("{}");
                context.Response.ContentType = "application/json";
                await next();
            });
        }
    }
}