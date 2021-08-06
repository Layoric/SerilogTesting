using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using Serilog;
using ServiceStack;
using SerilogTesting.ServiceInterface;
using ServiceStack.Logging;

namespace SerilogTesting
{
    public class Startup : ModularStartup
    {
        private static ILog Log = LogManager.GetLogger(typeof(Startup));
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Picks up logging configuration from `Serilog.Log.Logger` statics.
            app.UseSerilogRequestLogging();

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
            
            Log.Info("Warning in Startup.Configure");
        }
    }

    public class AppHost : AppHostBase
    {
        private static ILog Log = LogManager.GetLogger(typeof(AppHost));
        public AppHost() : base("SerilogTesting", typeof(MyServices).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });
            
            Log.Info("Warning in AppHost.");
        }
    }
}
