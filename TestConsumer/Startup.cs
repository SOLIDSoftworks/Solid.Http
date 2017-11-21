using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidHttp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SolidHttp.Abstractions;

namespace TestConsumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services
                .AddSolidHttpCore()
                .AddJson()
                .AddSolidHttpCoreOptions(options =>
                {
                    options.Events.OnRequestCreated += (sender, args) =>
                    {
                        var factory = args.Services.GetRequiredService<ISolidHttpClientFactory>();
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ApplicationServices.GetRequiredService<ISolidHttpInitializer>().Initialize();
            app.UseMvc();
        }
    }
}
