using System;
using System.IO;
using Dafaatir.Shared.Logger;
using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dafaatir.Shared.Env;

static public class EnvManager
{

  private static EnvData? _envData;

  public static EnvData GetEnvData()
  {


    if (_envData != null)
    {
      return _envData;
    }
    else
    {
      ILogger<EnvData> logger = AppLoggerFactory.LoggerFactory.CreateLogger<EnvData>();

      return new EnvData(logger);
    }
  }

  public static EnvData EnvData
  {
    get
    {
      if (_envData != null)
      {
        return _envData;
      }
      else
      {
        throw new Exception("");
      }
    }
    set
    {

      _envData = value;
    }
  }


  private static bool _isInitialized;

  public static void Initialize(string? _envFilePath = null)
  {
    if (_isInitialized) return;

    // Specify the .env file path or use default
    //string envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    string envFilePath = _envFilePath ?? Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env");


    if (File.Exists(envFilePath))
    {
      DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: true, envFilePaths: [envFilePath]));
    }
    else
    {
      // Handle the case where the .env file is not found (optional)
      Console.WriteLine($"Warning: .env file not found. Using default values. ${envFilePath}");
    }

    //var value = Environment.GetEnvironmentVariable("keyString");

    _isInitialized = true;
  }


  public static IServiceCollection AddEnvData(this IServiceCollection services)
  {

    services.AddSingleton<EnvData>(GetEnvData());

    return services;
  }



}

