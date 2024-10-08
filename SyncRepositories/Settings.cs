using Newtonsoft.Json;

namespace SyncRepositories;

public class Settings
{
    [JsonProperty("ConnectionStrings")] public ConnectionStrings ConnectionStrings { get; set; }

    [JsonProperty("Logging")] public Logging Logging { get; set; }

    [JsonProperty("AllowedHosts")] public string AllowedHosts { get; set; }
}

public class ConnectionStrings
{
    [JsonProperty("LocalOrderConnection")] public string LocalOrderConnection { get; set; }

    [JsonProperty("RemoteOrderConnection")]
    public string RemoteOrderConnection { get; set; }
}

public class Logging
{
    [JsonProperty("LogLevel")] public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    [JsonProperty("Default")] public string Default { get; set; }

    [JsonProperty("Microsoft.AspNetCore")] public string MicrosoftAspNetCore { get; set; }
}