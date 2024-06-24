using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Dafaatir.Shared.Env;

public enum DatabaseDriverEnum
{
    sqlite,
    postgres
}


public class EnvData
{
    private readonly ILogger<EnvData> _logger;

    public EnvData(ILogger<EnvData> logger)
    {
        logger.LogWarning("------------EnvData-----------");

        _logger = logger;
        EnvManager.EnvData = this;
    }


    private List<string>? _restrictedDomains = null; // Backing field for RestrictedDomains
    private bool? _allowCredentials; // Backing field for AllowCredentials
    private List<string>? _allowedOrigins;
    private List<string>? _allowedMethods;
    private List<string>? _allowedHeaders;
    private List<string>? _exposedHeaders;
    private DatabaseDriverEnum? _databaseDriver;
    private string? _databasePath;
    private List<string> GetStringListFromEnv(string variableName)
    {
        string? envValue = Environment.GetEnvironmentVariable(variableName);
        if (!string.IsNullOrEmpty(envValue))
        {
            return envValue.Split(',').Select(s => s.Trim()).ToList();
        }
        else
        {
            _logger.LogWarning($"Environment variable '{variableName}' not found. Returning an empty list.");
            return [];
        }
    }
    public List<string> AllowedOrigins
    {
        get
        {
            _allowedOrigins ??= GetStringListFromEnv("ALLOWED_ORIGINS");
            return _allowedOrigins;
        }
    }

    public List<string> AllowedMethods
    {
        get
        {
            _allowedMethods ??= GetStringListFromEnv("ALLOWED_METHODS");
            return _allowedMethods;
        }
    }

    public List<string> AllowedHeaders
    {
        get
        {
            _allowedHeaders ??= GetStringListFromEnv("ALLOWED_HEADERS");
            return _allowedHeaders;
        }
    }

    public List<string> ExposedHeaders
    {
        get
        {
            _exposedHeaders ??= GetStringListFromEnv("EXPOSED_HEADERS");
            return _exposedHeaders;
        }
    }

    public bool AllowCredentials
    {
        get
        {
            if (_allowCredentials.HasValue)
            {
                return _allowCredentials.Value;
            }

            string? envValue = Environment.GetEnvironmentVariable("ALLOW_CREDENTIALS");

            // Try parsing different formats: true/false, 1/0
            if (bool.TryParse(envValue, out bool boolResult))
            {
                _allowCredentials = boolResult;
            }
            else if (int.TryParse(envValue, out int intResult))
            {
                _allowCredentials = intResult == 1;
            }
            else
            {
                // Default to false if not found or invalid
                _allowCredentials = false;
                _logger.LogWarning("Environment variable 'ALLOW_CREDENTIALS' not found or invalid. Defaulting to 'false'.");
            }

            return _allowCredentials.Value;
        }
    }


    public List<string> RestrictedDomains
    {
        get
        {
            if (_restrictedDomains != null)
            {
                return _restrictedDomains;
            }

            string? domainsString = Environment.GetEnvironmentVariable("RESTRICT_DOMAINS");

            if (!string.IsNullOrEmpty(domainsString))
            {

                var domains = domainsString.Split(',').Select(d => d.Trim().ToLower()).ToList();

                if (domains.Contains("*"))
                {
                    _restrictedDomains = new List<string>();
                }
                else
                {
                    _restrictedDomains = domains;
                }

                _logger.LogInformation("Restricted domains loaded: {Domains}", string.Join(", ", _restrictedDomains));
            }
            else
            {
                _restrictedDomains = new List<string>(); // Or provide a default list
                _logger.LogWarning("Environment variable 'RESTRICT_DOMAINS' not set. Using an empty list.");
            }



            return _restrictedDomains;
        }
    }

    public DatabaseDriverEnum DatabaseDriver
    {
        get
        {

            if (_databaseDriver == null)
            {
                var DATABASE_DRIVER_ENV = Environment.GetEnvironmentVariable("DATABASE_DRIVER");
                if (string.IsNullOrEmpty(DATABASE_DRIVER_ENV))
                {
                    _databaseDriver = DatabaseDriverEnum.sqlite;
                    return DatabaseDriverEnum.sqlite;
                }
                else if (DATABASE_DRIVER_ENV == "sqlite")
                {
                    _databaseDriver = DatabaseDriverEnum.sqlite;
                    return DatabaseDriverEnum.sqlite;
                }
                else if (DATABASE_DRIVER_ENV == "postgres")
                {
                    _databaseDriver = DatabaseDriverEnum.postgres;
                    return DatabaseDriverEnum.postgres;
                }
                else
                {
                    _databaseDriver = DatabaseDriverEnum.sqlite;
                    return DatabaseDriverEnum.sqlite;
                }



            }
            else
            {
                return (DatabaseDriverEnum)_databaseDriver;
            }







        }
    }

    public string? DatabasePath
    {
        get
        {
            _databasePath ??= Environment.GetEnvironmentVariable("DATABASE_PATH");

            /*if (string.IsNullOrEmpty(_databasePath))
            {
                _logger.LogError("Environment variable 'DATABASE_PATH' not found.");
                throw new InvalidOperationException("Environment variable 'DATABASE_PATH' is required.");
            }*/

            return _databasePath;
        }
    }
}
