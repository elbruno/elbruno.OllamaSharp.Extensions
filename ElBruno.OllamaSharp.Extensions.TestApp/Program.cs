using ElBruno.OllamaSharp.Extensions;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

Console.WriteLine("=== OllamaSharp Extensions Demo - C# 14-Ready Features ===");
Console.WriteLine();

// Create an OllamaApiClient with default settings
var ollamaClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2");

Console.WriteLine($"Default timeout: {ollamaClient.GetTimeout()}");
Console.WriteLine();

// ===== C# 14-Ready Fluent Configuration =====
Console.WriteLine("--- C# 14-Ready Fluent Configuration ---");
Console.WriteLine();

// Example 1: Fluent initialization with timeout
var fluentClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .SetTimeout(TimeSpan.FromMinutes(5));

Console.WriteLine($"Fluent client timeout: {fluentClient.GetTimeout()}");

// Example 2: Using builder-style extension methods
var quickClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .WithQuickTimeout();  // 2 minutes

Console.WriteLine($"Quick client timeout: {quickClient.GetTimeout()}");

var standardClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .WithStandardTimeout();  // 5 minutes

Console.WriteLine($"Standard client timeout: {standardClient.GetTimeout()}");

var longClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .WithLongTimeout();  // 10 minutes

Console.WriteLine($"Long client timeout: {longClient.GetTimeout()}");
Console.WriteLine();

// Example 3: Advanced configuration with functional approach
Console.WriteLine("--- Advanced C# 14-Ready Functional Configuration ---");
var configuredClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .ConfigureTimeout(current => 
    {
        // Double the current timeout, or set to 5 minutes if not set
        return current.HasValue ? current.Value * 2 : TimeSpan.FromMinutes(5);
    });

Console.WriteLine($"Configured client timeout: {configuredClient.GetTimeout()}");
Console.WriteLine();

// ===== Traditional Extension Method Syntax (still supported) =====
Console.WriteLine("--- Traditional Extension Methods (backward compatible) ---");
ollamaClient.SetTimeout(TimeSpan.FromMinutes(10));
Console.WriteLine($"Traditional method timeout: {ollamaClient.GetTimeout()}");
Console.WriteLine();

// Demonstrate usage with Microsoft Agent Framework
Console.WriteLine("=== Microsoft Agent Framework Integration ===");
Console.WriteLine();

try
{
    // Configure client with extended timeout for AI agent
    var agentClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
        .WithStandardTimeout();  // C# 14-ready builder style

    // Cast to IChatClient for Agent Framework compatibility
    IChatClient chatClient = agentClient;

    var writerAgent = chatClient.CreateAIAgent(
        name: "Writer",
        instructions: "Write short stories that are engaging and creative, and always add bad jokes to them.");

    Console.WriteLine("Agent created successfully with C# 14-ready timeout configuration!");
    Console.WriteLine($"Agent timeout: {agentClient.GetTimeout()}");
    Console.WriteLine();
    
    // Note: This will only work if you have Ollama running locally with the llama3.2 model
    Console.WriteLine("Attempting to run agent (requires Ollama running locally)...");
    
    var response = await writerAgent.RunAsync("Write a very short story about a developer learning C# 14 features.");
    
    Console.WriteLine("Agent Response:");
    Console.WriteLine(response.Text);
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Connection error: {ex.Message}");
    Console.WriteLine("Make sure Ollama is running locally on port 11434 with the llama3.2 model.");
}
catch (TaskCanceledException ex)
{
    Console.WriteLine($"Request timed out: {ex.Message}");
    Console.WriteLine($"The request exceeded the configured timeout.");
    Console.WriteLine("Consider using .WithExtendedTimeout() for long-running requests.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Demo Complete ===");
Console.WriteLine();
Console.WriteLine("💡 C# 14-Ready Features Demonstrated:");
Console.WriteLine("   ✓ Fluent method chaining: .SetTimeout().AnotherMethod()");
Console.WriteLine("   ✓ Builder-style extensions: .WithQuickTimeout(), .WithStandardTimeout()");
Console.WriteLine("   ✓ Functional configuration: .ConfigureTimeout(current => ...)");
Console.WriteLine("   ✓ Expression-bodied members: => client.SetTimeout(...)");
Console.WriteLine();
Console.WriteLine("🚀 Future: When C# 14 extension members are fully released, this can use:");
Console.WriteLine("   extension(OllamaApiClient client) { public TimeSpan? Timeout { get; set; } }");
