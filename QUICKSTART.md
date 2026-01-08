# Quick Start Guide

Get started with OllamaSharp Extensions in just a few minutes!

## Prerequisites

- .NET 10 SDK installed
- Ollama running locally (optional, for running test app)
- Basic knowledge of C# and OllamaSharp

## Step 1: Add OllamaSharp package

```bash
dotnet add package OllamaSharp
```

## Step 2: Add the OllamaSharp Extension Library

- Add Nuget Reference

## Step 3: Use the Extension

### Basic Example

```csharp
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

// Create client and set timeout in one line
var client = new OllamaApiClient(
    new Uri("http://localhost:11434"), 
    "llama3.2")
    .SetTimeout(TimeSpan.FromMinutes(5));

// Verify the timeout
Console.WriteLine($"Timeout: {client.GetTimeout()}");
```

### With Microsoft Agent Framework

```csharp
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

var client = new OllamaApiClient(
    new Uri("http://localhost:11434/"), 
    "llama3.2")
    .SetTimeout(TimeSpan.FromMinutes(10));

IChatClient chatClient = client;

var agent = chatClient.CreateAIAgent(
    name: "Assistant",
    instructions: "You are a helpful assistant.");

var response = await agent.RunAsync("Hello!");
Console.WriteLine(response.Text);
```

## Common Timeouts Guide

Choose an appropriate timeout based on your use case:

| Use Case | Recommended Timeout | Example |
|----------|-------------------|---------|
| Quick queries | 1-2 minutes | "What is 2+2?" |
| Standard prompts | 5 minutes | "Write a short story" |
| Long-form content | 10+ minutes | "Write a detailed essay" |
| Large models on slow hardware | 15-30 minutes | Running Llama 70B locally |

### Setting Timeouts

```csharp
// Quick queries
client.SetTimeout(TimeSpan.FromMinutes(2));

// Standard prompts
client.SetTimeout(TimeSpan.FromMinutes(5));

// Long-form generation
client.SetTimeout(TimeSpan.FromMinutes(10));

// Very large models or slow hardware
client.SetTimeout(TimeSpan.FromMinutes(30));
```

## Troubleshooting

### Extension Method Not Found

Make sure you have the using directive:

```csharp
using ElBruno.OllamaSharp.Extensions;
```

### Still Timing Out

Verify the timeout is set:

```csharp
var timeout = client.GetTimeout();
Console.WriteLine($"Current timeout: {timeout}");
```

Increase the timeout if needed:

```csharp
client.SetTimeout(TimeSpan.FromMinutes(15));
```

## Next Steps

- Read the [README.md](README.md) for comprehensive documentation
- Check the [test application](OllamaSharpExtensions.TestApp/Program.cs) for code examples
- - Check the [Microsoft Agent Framework test application](ElBruno.OllamaSharp.Extensions.TestMAF/Program.cs) for code examples

## Support

- **Issues**: [GitHub Issues](https://github.com/elbruno/elbruno.OllamaSharp.Extensions/issues)
- **Author**: [Bruno Capuano](https://github.com/elbruno)

---

Happy coding! 🚀
