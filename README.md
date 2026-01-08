# ElBruno.OllamaSharp.Extensions

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C#](https://img.shields.io/badge/C%23-14--Ready-239120)](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
[![NuGet](https://img.shields.io/nuget/v/ElBruno.OllamaSharp.Extensions.svg)](https://www.nuget.org/packages/ElBruno.OllamaSharp.Extensions)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![CI Build](https://github.com/elbruno/elbruno.OllamaSharp.Extensions/workflows/CI%20Build%20and%20Test/badge.svg)](https://github.com/elbruno/elbruno.OllamaSharp.Extensions/actions)

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

### NuGet Package

```bash
dotnet add package ElBruno.OllamaSharp.Extensions
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

### Builder Pattern Extensions (Recommended)

```csharp
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

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

#### Future C# 14 Extension Member Syntax (Work in Progress)

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

**Reference:** [C# 14 Extension Members Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members)

### With Microsoft Agent Framework

```csharp
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

// Create and configure the OllamaApiClient
var ollamaClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2")
    .SetTimeout(TimeSpan.FromMinutes(5));

// Use with Agent Framework
var writerAgent = ollamaClient.CreateAIAgent(
    name: "Writer",
    instructions: "Write engaging and creative stories.");

var response = await writerAgent.RunAsync("Tell me a story about AI.");
Console.WriteLine(response.Text);
```

## 🔧 Implementation Details

### How It Works

The extension library uses reflection to access the private `HttpClient` instance within `OllamaApiClient` and modifies its `Timeout` property. This approach:

1. **Preserves Encapsulation**: Doesn't require modifying OllamaSharp's source code
2. **Type-Safe**: Provides compile-time safety through extension methods
3. **Minimal Overhead**: Direct property access with no performance impact
4. **Compatible**: Works with existing OllamaSharp versions (tested with 5.4.12)

## 🧪 Test Applications

The included test applicatiosn demonstrates all features of the extension library in a Simple Console Application and also using Microsoft Agent Framework.

## 🤝 Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

## 🙏 Acknowledgments

- **OllamaSharp**: Original library by [awaescher](https://github.com/awaescher/OllamaSharp)
- **Issue Reporter**: [manveldavid](https://github.com/manveldavid) for identifying the timeout issue
- **Microsoft Agent Framework**: For providing excellent AI agent abstractions

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/elbruno/elbruno.OllamaSharp.Extensions/issues)
- **Author**: [Bruno Capuano](https://github.com/elbruno)
- **Blog**: [elbruno.com](https://www.elbruno.com)

---

<p align="center">
  <strong>Built with ❤️ for the .NET community</strong>
</p>
