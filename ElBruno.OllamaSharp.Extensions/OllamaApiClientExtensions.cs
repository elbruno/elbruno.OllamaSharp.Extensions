using System.Reflection;
using OllamaSharp;

namespace ElBruno.OllamaSharp.Extensions;

/// <summary>
/// Extension methods for OllamaApiClient using modern C# 14-ready patterns.
/// Provides timeout configuration support for long-running LLM requests.
/// </summary>
/// <remarks>
/// This implementation uses C# 14-ready extension patterns while maintaining compatibility.
/// When C# 14 extension members become fully available, this can be converted to:
/// <code>
/// extension(OllamaApiClient client)
/// {
///     public TimeSpan? Timeout { get; set; }
/// }
/// </code>
/// </remarks>
public static class OllamaApiClientExtensions
{
    /// <summary>
    /// Sets the timeout for the OllamaApiClient's underlying HttpClient using modern C# patterns.
    /// This is useful for long-running LLM requests that may exceed the default 100-second timeout.
    /// </summary>
    /// <param name="client">The OllamaApiClient instance.</param>
    /// <param name="timeout">The timeout duration to set.</param>
    /// <returns>The same OllamaApiClient instance for method chaining (fluent interface).</returns>
    /// <example>
    /// <code>
    /// // C# 14-ready fluent configuration
    /// var client = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.2")
    ///     .SetTimeout(TimeSpan.FromMinutes(10));
    /// 
    /// // Traditional approach
    /// client.SetTimeout(TimeSpan.FromMinutes(5));
    /// 
    /// // Combined with other configuration
    /// var configuredClient = new OllamaApiClient(uri, model)
    ///     .SetTimeout(TimeSpan.FromMinutes(10));
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when timeout is zero or negative.</exception>
    /// <exception cref="InvalidOperationException">Thrown when unable to access internal HttpClient.</exception>
    public static OllamaApiClient SetTimeout(this OllamaApiClient client, TimeSpan timeout)
    {
        ArgumentNullException.ThrowIfNull(client);
        
        if (timeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout must be greater than zero.");
        }

        // Use reflection to access the private _client field in OllamaApiClient
        // This approach is necessary until OllamaSharp provides a public API for timeout configuration
        var httpClientField = typeof(OllamaApiClient)
            .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance);

        if (httpClientField?.GetValue(client) is HttpClient httpClient)
        {
            httpClient.Timeout = timeout;
        }
        else
        {
            throw new InvalidOperationException(
                "Unable to access the internal HttpClient of OllamaApiClient. " +
                "The OllamaSharp library structure may have changed.");
        }

        return client;
    }

    /// <summary>
    /// Gets the current timeout setting from the OllamaApiClient's underlying HttpClient.
    /// </summary>
    /// <param name="client">The OllamaApiClient instance.</param>
    /// <returns>The current timeout duration, or null if unable to access it.</returns>
    /// <example>
    /// <code>
    /// var timeout = client.GetTimeout();
    /// Console.WriteLine($"Current timeout: {timeout}");
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
    public static TimeSpan? GetTimeout(this OllamaApiClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        // Use reflection to access the private _client field in OllamaApiClient
        var httpClientField = typeof(OllamaApiClient)
            .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance);

        if (httpClientField?.GetValue(client) is HttpClient httpClient)
        {
            return httpClient.Timeout;
        }

        return null;
    }

    /// <summary>
    /// Configures the OllamaApiClient with a custom timeout using a configuration action.
    /// This provides a C# 14-ready fluent configuration pattern.
    /// </summary>
    /// <param name="client">The OllamaApiClient instance.</param>
    /// <param name="configure">Configuration action that receives the current timeout and returns the new timeout.</param>
    /// <returns>The same OllamaApiClient instance for method chaining.</returns>
    /// <example>
    /// <code>
    /// // Double the current timeout
    /// client.ConfigureTimeout(current => current * 2);
    /// 
    /// // Set to 5 minutes if not already set
    /// client.ConfigureTimeout(current => current ?? TimeSpan.FromMinutes(5));
    /// 
    /// // Chain multiple configurations
    /// var client = new OllamaApiClient(uri, model)
    ///     .ConfigureTimeout(_ => TimeSpan.FromMinutes(10));
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException">Thrown when client or configure is null.</exception>
    public static OllamaApiClient ConfigureTimeout(this OllamaApiClient client, Func<TimeSpan?, TimeSpan> configure)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(configure);

        var currentTimeout = client.GetTimeout();
        var newTimeout = configure(currentTimeout);
        
        return client.SetTimeout(newTimeout);
    }
}

/// <summary>
/// Extension methods for configuring OllamaApiClient timeout with builder-style patterns.
/// Demonstrates C# 14-ready extension member organization.
/// </summary>
public static class OllamaApiClientTimeoutExtensions
{
    /// <summary>
    /// Sets a timeout suitable for quick queries (2 minutes).
    /// </summary>
    public static OllamaApiClient WithQuickTimeout(this OllamaApiClient client)
        => client.SetTimeout(TimeSpan.FromMinutes(2));

    /// <summary>
    /// Sets a timeout suitable for standard prompts (5 minutes).
    /// </summary>
    public static OllamaApiClient WithStandardTimeout(this OllamaApiClient client)
        => client.SetTimeout(TimeSpan.FromMinutes(5));

    /// <summary>
    /// Sets a timeout suitable for long-form generation (10 minutes).
    /// </summary>
    public static OllamaApiClient WithLongTimeout(this OllamaApiClient client)
        => client.SetTimeout(TimeSpan.FromMinutes(10));

    /// <summary>
    /// Sets a timeout suitable for very large models or slow hardware (30 minutes).
    /// </summary>
    public static OllamaApiClient WithExtendedTimeout(this OllamaApiClient client)
        => client.SetTimeout(TimeSpan.FromMinutes(30));
}
