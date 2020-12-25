using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace TNE.Common.Logger
{
    public static partial class SerilogExtensions
    {
               
        public static IWebHostBuilder UseTneSerilog(this IWebHostBuilder hostBuilder)
        {
                     
            string[] arrAppName = Assembly.GetEntryAssembly().GetName().Name.Split('.');
            string appName = arrAppName.Length > 1 ? arrAppName[1] : arrAppName[0];

            hostBuilder.UseTneSerilog( appName);

            return hostBuilder;
        }

       
        public static IWebHostBuilder UseTneSerilog(this IWebHostBuilder hostBuilder, string applicationName)
        {
          
            string date = DateTime.Now.ToString("yyyyMMdd");

            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration) // на 10.01.2020 берем только MinimumLevel, остальное определяем в коде. 
                .Enrich.FromLogContext()
                .Enrich.WithMachineName() 
                .Enrich.WithProperty("ServiceName", applicationName)
                .WriteTo.Console()
                .WriteTo.File($"Logs/{applicationName}-{date}.txt",
                Serilog.Events.LogEventLevel.Verbose,
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{ServiceName}] [{Level}] {Message}  {ActionName}  {NewLine} {Exception}"));
                

            return hostBuilder;
        }
    }
}
