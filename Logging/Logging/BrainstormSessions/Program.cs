using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Email;
using System;
using System.Net;

namespace BrainstormSessions;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                formatter: new CompactJsonFormatter(),
                path: "./Logs/brainstormSessions.txt",
                rollingInterval: RollingInterval.Day,
                shared: true)
            .WriteTo.Email(new EmailConnectionInfo
            {
                FromEmail = "sampleemail@gmail.com",
                ToEmail = "anotheremail@gmail.com",
                MailServer = "yoursmtpserver",
                NetworkCredentials = new NetworkCredential("yourusername", "yourpassword"),
                EnableSsl = true,
                Port = 465,
                EmailSubject = "Log Infos"
            },
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
            batchPostingLimit: 10
            ).CreateLogger();
        
        try
        {
            Log.Information("Starting up");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .UseSerilog()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
}
