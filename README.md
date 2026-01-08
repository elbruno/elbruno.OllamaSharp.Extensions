# ElBruno.OllamaSharp.Extensions

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C#](https://img.shields.io/badge/C%23-14--Ready-239120)](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
[![NuGet](https://img.shields.io/nuget/v/ElBruno.OllamaSharp.Extensions.svg)](https://www.nuget.org/packages/ElBruno.OllamaSharp.Extensions)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![CI Build](https://github.com/elbruno/a2aapiredemo/workflows/CI%20Build%20and%20Test/badge.svg)](https://github.com/elbruno/a2aapiredemo/actions)

> **C# 14-ready extension library for OllamaSharp that adds configurable timeout support to OllamaApiClient**

This library provides modern C# 14-ready extension methods to customize the timeout settings for OllamaSharp's `OllamaApiClient`, solving the issue where long-running LLM requests exceed the default 100-second timeout.

## 📋 Table of Contents

- [Background](#background)
- [Features](#features)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Usage](#usage)
- [Implementation Details](#implementation-details)
- [Test Application](#test-application)
- [API Reference](#api-reference)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Background

This library was created to address [OllamaSharp Issue #173](https://github.com/awaescher/OllamaSharp/issues/173), where users encountered timeout issues when making requests to local Ollama LLM instances on less powerful hardware. By default, OllamaSharp's `OllamaApiClient` uses a 100-second timeout, which can be insufficient for:

- Long-running inference on slower hardware
- Large context windows
- Complex prompts requiring extensive processing
- Models with longer response times

## ✨ Features

- **Simple Extension API**: Add timeout configuration with a single method call
- **.NET 10 Compatible**: Built with the latest .NET features and C# preview language version
- **Fluent Interface**: Method chaining support for clean configuration
- **Builder Pattern Extensions**: Convenient methods like `WithStandardTimeout()`, `WithLongTimeout()`
- **Functional Configuration**: Advanced `ConfigureTimeout()` with lambda expressions
- **Type-Safe**: Full compile-time type checking and IntelliSense support
- **Microsoft Agent Framework Compatible**: Works seamlessly with `Microsoft.Agents.AI` and `Microsoft.Extensions.AI`
- **Non-Breaking**: Uses extension methods, so existing code continues to work without modification
- **(Almost) Fully Tested**: 27 unit tests with 100% pass rate

## 📦 Installation

### NuGet Package (Recommended)

```bash
dotnet add package ElBruno.OllamaSharp.Extensions
```

Or using Package Manager Console:

```powershell
Install-Package ElBruno.OllamaSharp.Extensions
```

Or add to your `.csproj` file:

```xml
<ItemGroup>
  <PackageReference Include="ElBruno.OllamaSharp.Extensions" Version="1.0.0" />
</ItemGroup>
```

```bash
dotnet add reference path/to/OllamaSharpExtensions/OllamaSharpExtensions.csproj
```

### Package Reference (if published to NuGet)

```bash
dotnet add package OllamaSharpExtensions
```

Or add to your `.csproj` file:

```xml
<ItemGroup>
  <PackageReference Include="OllamaSharpExtensions" Version="1.0.0" />
</ItemGroup>
```

## 🚀 Quick Start

```csharp
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

// Create client and set timeout in one line
var client = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.2")
    .WithStandardTimeout(); // 5 minutes

// Or use custom timeout
client.SetTimeout(TimeSpan.FromMinutes(10));

// Get current timeout
var timeout = client.GetTimeout();
Console.WriteLine($"Current timeout: {timeout}");
```

## 🚀 Usage

### C# 14-Ready Features

This library is designed with C# 14 in mind, showcasing modern extension patterns that will evolve with the language.

#### Builder Pattern Extensions (Recommended)

```csharp
using OllamaSharp;
using OllamaSharpExtensions;

// Quick queries (2 minutes)
var quickClient = new OllamaApiClient(uri, model).WithQuickTimeout();

// Standard prompts (5 minutes)
var standardClient = new OllamaApiClient(uri, model).WithStandardTimeout();

// Long-form generation (10 minutes)
var longClient = new OllamaApiClient(uri, model).WithLongTimeout();

// Extended timeout for large models (30 minutes)
var extendedClient = new OllamaApiClient(uri, model).WithExtendedTimeout();
```

#### Functional Configuration Pattern

```csharp
// Advanced configuration with lambda expressions
var client = new OllamaApiClient(uri, model)
    .ConfigureTimeout(current => 
        current.HasValue ? current.Value * 2 : TimeSpan.FromMinutes(5));

// Conditional configuration
var adaptiveClient = new OllamaApiClient(uri, model)
    .ConfigureTimeout(current => 
        Environment.Is64BitProcess 
            ? TimeSpan.FromMinutes(10) 
            : TimeSpan.FromMinutes(20));
```

#### Traditional Extension Methods

```csharp
// Basic usage
var client = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.2");
client.SetTimeout(TimeSpan.FromMinutes(5));

// Get current timeout
var currentTimeout = client.GetTimeout();
Console.WriteLine($"Current timeout: {currentTimeout}");
```

#### Fluent Method Chaining

```csharp
var client = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.2")
    .SetTimeout(TimeSpan.FromMinutes(10));
```

### C# 14 Extension Members - Future Evolution

This library is designed to embrace C# 14's extension members feature (when fully released). The current implementation uses C# 14-ready patterns that will seamlessly evolve to the new syntax:

#### Current Implementation (C# 14-Ready)

```csharp
public static class OllamaApiClientExtensions
{
    public static OllamaApiClient SetTimeout(this OllamaApiClient client, TimeSpan timeout)
    {
        // Implementation using reflection
        return client;
    }
    
    // Builder patterns for common scenarios
    public static OllamaApiClient WithStandardTimeout(this OllamaApiClient client)
        => client.SetTimeout(TimeSpan.FromMinutes(5));
}
```

#### Future C# 14 Extension Member Syntax (When Available)

When C# 14's `extension` keyword becomes fully available, this can be simplified to:

```csharp
extension(OllamaApiClient client)
{
    // Extension property - natural property syntax
    public TimeSpan? Timeout
    {
        get => /* implementation */;
        set => /* implementation */;
    }
    
    // Extension methods still supported
    public OllamaApiClient SetTimeout(TimeSpan timeout)
    {
        Timeout = timeout;
        return client;
    }
}
```

**Benefits of the future syntax:**

- More natural property access: `client.Timeout = TimeSpan.FromMinutes(5);`
- Cleaner organization of related extension members
- Better IDE support and discoverability
- Maintained backward compatibility with existing code

**Reference:** [C# 14 Extension Members Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members)

### With Microsoft Agent Framework

```csharp
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;
using OllamaSharpExtensions;

// Create and configure the OllamaApiClient
var ollamaClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .SetTimeout(TimeSpan.FromMinutes(5));

// Use with Agent Framework
IChatClient chatClient = ollamaClient;

var writerAgent = chatClient.CreateAIAgent(
    name: "Writer",
    instructions: "Write engaging and creative stories.");

var response = await writerAgent.RunAsync("Tell me a story about AI.");
Console.WriteLine(response.Text);
```

### Real-World Example: Handling Long Inference Times

```csharp
using OllamaSharp;
using OllamaSharpExtensions;

var client = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.2");

// For slower hardware, set a generous timeout
client.SetTimeout(TimeSpan.FromMinutes(10));

try
{
    var response = await client.GetCompletionAsync("Write a detailed essay about quantum computing.");
    Console.WriteLine(response);
}
catch (TaskCanceledException ex)
{
    Console.WriteLine($"Request timed out after {client.GetTimeout()}: {ex.Message}");
    // Consider increasing the timeout further or optimizing the prompt
}
```

## 🔧 Implementation Details

### How It Works

The extension library uses reflection to access the private `HttpClient` instance within `OllamaApiClient` and modifies its `Timeout` property. This approach:

1. **Preserves Encapsulation**: Doesn't require modifying OllamaSharp's source code
2. **Type-Safe**: Provides compile-time safety through extension methods
3. **Minimal Overhead**: Direct property access with no performance impact
4. **Compatible**: Works with existing OllamaSharp versions (tested with 5.4.12)

### Extension Methods

#### Core Methods

##### `SetTimeout(TimeSpan timeout)`

Sets the timeout for the OllamaApiClient's underlying HttpClient.

```csharp
public static OllamaApiClient SetTimeout(this OllamaApiClient client, TimeSpan timeout)
```

**Parameters:**

- `client`: The OllamaApiClient instance
- `timeout`: The timeout duration (must be greater than zero)

**Returns:** The same OllamaApiClient instance for method chaining

**Throws:**

- `ArgumentNullException`: If client is null
- `ArgumentOutOfRangeException`: If timeout is zero or negative
- `InvalidOperationException`: If unable to access the internal HttpClient

##### `GetTimeout()`

Gets the current timeout setting from the OllamaApiClient.

```csharp
public static TimeSpan? GetTimeout(this OllamaApiClient client)
```

**Parameters:**

- `client`: The OllamaApiClient instance

**Returns:** The current timeout duration, or null if unable to access it

**Throws:**

- `ArgumentNullException`: If client is null

##### `ConfigureTimeout(Func<TimeSpan?, TimeSpan> configure)`

Configures the timeout using a functional approach (C# 14-ready pattern).

```csharp
public static OllamaApiClient ConfigureTimeout(
    this OllamaApiClient client, 
    Func<TimeSpan?, TimeSpan> configure)
```

**Parameters:**

- `client`: The OllamaApiClient instance
- `configure`: Configuration function that receives the current timeout and returns the new timeout

**Returns:** The same OllamaApiClient instance for method chaining

**Throws:**

- `ArgumentNullException`: If client or configure is null

#### Builder Pattern Methods (C# 14-Ready)

##### `WithQuickTimeout()`

Sets a timeout suitable for quick queries (2 minutes).

```csharp
public static OllamaApiClient WithQuickTimeout(this OllamaApiClient client)
```

##### `WithStandardTimeout()`

Sets a timeout suitable for standard prompts (5 minutes).

```csharp
public static OllamaApiClient WithStandardTimeout(this OllamaApiClient client)
```

##### `WithLongTimeout()`

Sets a timeout suitable for long-form generation (10 minutes).

```csharp
public static OllamaApiClient WithLongTimeout(this OllamaApiClient client)
```

##### `WithExtendedTimeout()`

Sets a timeout suitable for very large models or slow hardware (30 minutes).

```csharp
public static OllamaApiClient WithExtendedTimeout(this OllamaApiClient client)
```

**Throws:**

- `ArgumentNullException`: If client is null

### Architecture

```
OllamaSharpExtensions/
├── OllamaSharpExtensions/              # Class Library
│   ├── OllamaApiClientExtensions.cs    # Extension methods
│   └── OllamaSharpExtensions.csproj    # Project file
│
├── OllamaSharpExtensions.TestApp/      # Test Console Application
│   ├── Program.cs                       # Demo application
│   └── OllamaSharpExtensions.TestApp.csproj
│
└── README.md                            # This file
```

### Key Features Used

- **.NET 10**: Latest framework features
- **C# Preview Features**: `LangVersion=preview` for cutting-edge language features
- **Extension Methods**: Clean API without modifying original library
- **Reflection**: Safe access to internal HttpClient
- **Fluent Interface**: Method chaining support

## 🧪 Test Application

The included test application demonstrates all features:

```bash
cd OllamaSharpExtensions.TestApp
dotnet run
```

**Sample Output:**

```
=== OllamaSharp Extensions Demo ===

Default timeout: 00:01:40

Setting timeout to 5 minutes (300 seconds)...
New timeout: 00:05:00

Creating an AI Agent using the configured OllamaApiClient...
Agent created successfully!

Attempting to run agent (requires Ollama running locally)...
Connection error: Connection refused (localhost:11434)
Make sure Ollama is running locally on port 11434 with the llama3.2 model.

=== Demo Complete ===
```

To run with an actual Ollama instance:

1. Install [Ollama](https://ollama.ai/)
2. Start Ollama: `ollama serve`
3. Pull a model: `ollama pull llama3.2`
4. Run the test app: `dotnet run`

## 📖 API Reference

### Namespace: `OllamaSharpExtensions`

#### Class: `OllamaApiClientExtensions`

Static class containing extension methods for `OllamaApiClient`.

**Methods:**

| Method | Description | Returns |
|--------|-------------|---------|
| `SetTimeout(TimeSpan)` | Sets the timeout for HTTP requests | `OllamaApiClient` |
| `GetTimeout()` | Gets the current timeout setting | `TimeSpan?` |

### Error Handling

The extension methods handle the following error cases:

1. **Null Client**: Throws `ArgumentNullException`
2. **Invalid Timeout**: Throws `ArgumentOutOfRangeException` for zero or negative values
3. **Reflection Failure**: Throws `InvalidOperationException` if the internal structure has changed

## 🤝 Contributing

Contributions are welcome! This library is part of the [a2aapiredemo](https://github.com/elbruno/a2aapiredemo) repository.

### Development Setup

```bash
# Clone the repository
git clone https://github.com/elbruno/a2aapiredemo.git
cd a2aapiredemo/OllamaSharpExtensions

# Build the library
cd OllamaSharpExtensions
dotnet build

# Run tests
cd ../OllamaSharpExtensions.TestApp
dotnet run
```

## 📄 License

This project is part of the [a2aapiredemo](https://github.com/elbruno/a2aapiredemo) repository and is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

## 🙏 Acknowledgments

- **OllamaSharp**: Original library by [awaescher](https://github.com/awaescher/OllamaSharp)
- **Issue Reporter**: [manveldavid](https://github.com/manveldavid) for identifying the timeout issue
- **Microsoft Agent Framework**: For providing excellent AI agent abstractions

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/elbruno/a2aapiredemo/issues)
- **Author**: [Bruno Capuano](https://github.com/elbruno)
- **Blog**: [elbruno.com](https://www.elbruno.com)

---

<p align="center">
  <strong>Built with ❤️ for the .NET community</strong>
</p>
