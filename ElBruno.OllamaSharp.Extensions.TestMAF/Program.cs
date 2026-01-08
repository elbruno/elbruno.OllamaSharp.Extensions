using ElBruno.OllamaSharp.Extensions;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

var ollamaClient =
    new OllamaApiClient(new Uri("http://localhost:11434/"), "qwen3-vl");

// Sample using too little time to trigger an error
ollamaClient.SetTimeout(TimeSpan.FromSeconds(3));

// 5 minutes to handle long running tasks, such as generating long stories or complex responses
// ollamaClient.SetTimeout(TimeSpan.FromMinutes(5));

AIAgent writer = ollamaClient.CreateAIAgent(
    name: "Writer",
    instructions: "Write short stories that are engaging and creative, and always add bad jokes to them.");

AgentRunResponse response = await writer.RunAsync("Write a long story about Lima Peru en Spanish");

Console.WriteLine(response.Text);


