





using System.Net;
using Dafaatir.Shared.Env;
using DotNetEnv;
using dotenv.net;
using Dafaatir.Shared.Logger;
namespace Dafaatir.Bootstrapper;

public class Program
{

    public static async Task Main(string[] args)
    {


        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {



        var host = Host.CreateDefaultBuilder(args);

        EnvManager.Initialize();
        host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddEnvironmentVariables();
            //config.AddJsonFile("appsettings.json", optional: true);

            //config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);

            
        });

        host.ConfigureLogging(logging =>
        {

            // Use the pre-configured LoggerFactory
            logging.Services.AddSingleton<ILoggerFactory>(AppLoggerFactory.LoggerFactory);



         
        });

        host.ConfigureWebHostDefaults((webBuilder) =>
        {
            var logger = AppLoggerFactory.LoggerFactory.CreateLogger<EnvData>();
            EnvData envdata = EnvManager.GetEnvData();

            webBuilder.ConfigureKestrel(serverOptions =>
            {
                serverOptions.AddServerHeader = false;
                //serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                //{
                //httpsOptions.ServerCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2("path/to/your/certificate.pfx", "your_certificate_password"); 
                //httpsOptions.
                // This will automatically attempt to use the ASP.NET Core
                // development certificate if available (typical in development)
                // For production, you might need to set the certificate explicitly
                // using httpsOptions.ServerCertificate = ... as shown in the previous example 
                //});
            });
            webBuilder.UseKestrelHttpsConfiguration(

            );
            webBuilder.UseStartup<Startup>();
        });



        return host;








    }
}


