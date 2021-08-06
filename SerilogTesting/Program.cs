using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;

namespace SerilogTesting
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        
        public static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
            LogManager.LogFactory = new SerilogFactory(logger);
            Serilog.Log.Logger = logger;
            try
            {
                Log.Info("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // Picks up default logger from Serilog.Log.Logger global.
                .UseSerilog()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseModularStartup<Startup>();
                });
    }
}
