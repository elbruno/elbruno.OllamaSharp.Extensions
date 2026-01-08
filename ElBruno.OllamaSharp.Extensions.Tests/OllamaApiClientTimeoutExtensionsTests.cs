using Microsoft.VisualStudio.TestTools.UnitTesting;
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

namespace ElBruno.OllamaSharp.Extensions.Tests;

[TestClass]
public class OllamaApiClientTimeoutExtensionsTests
{
    private OllamaApiClient? _testClient;

    [TestInitialize]
    public void Setup()
    {
        _testClient = new OllamaApiClient(new Uri("http://localhost:11434"), "test-model");
    }

    [TestMethod]
    public void WithQuickTimeout_SetsTimeoutTo2Minutes()
    {
        // Act
        var result = _testClient!.WithQuickTimeout();

        // Assert
        Assert.AreEqual(_testClient, result);
        var timeout = _testClient.GetTimeout();
        Assert.IsNotNull(timeout);
        Assert.AreEqual(TimeSpan.FromMinutes(2), timeout.Value);
    }

    [TestMethod]
    public void WithStandardTimeout_SetsTimeoutTo5Minutes()
    {
        // Act
        var result = _testClient!.WithStandardTimeout();

        // Assert
        Assert.AreEqual(_testClient, result);
        var timeout = _testClient.GetTimeout();
        Assert.IsNotNull(timeout);
        Assert.AreEqual(TimeSpan.FromMinutes(5), timeout.Value);
    }

    [TestMethod]
    public void WithLongTimeout_SetsTimeoutTo10Minutes()
    {
        // Act
        var result = _testClient!.WithLongTimeout();

        // Assert
        Assert.AreEqual(_testClient, result);
        var timeout = _testClient.GetTimeout();
        Assert.IsNotNull(timeout);
        Assert.AreEqual(TimeSpan.FromMinutes(10), timeout.Value);
    }

    [TestMethod]
    public void WithExtendedTimeout_SetsTimeoutTo30Minutes()
    {
        // Act
        var result = _testClient!.WithExtendedTimeout();

        // Assert
        Assert.AreEqual(_testClient, result);
        var timeout = _testClient.GetTimeout();
        Assert.IsNotNull(timeout);
        Assert.AreEqual(TimeSpan.FromMinutes(30), timeout.Value);
    }

    [TestMethod]
    public void BuilderMethods_SupportFluentChaining()
    {
        // Act
        var client = new OllamaApiClient(new Uri("http://localhost:11434"), "test-model")
            .WithStandardTimeout();

        // Assert
        Assert.IsNotNull(client);
        var timeout = client.GetTimeout();
        Assert.AreEqual(TimeSpan.FromMinutes(5), timeout);
    }

    [TestMethod]
    public void BuilderMethods_CanBeChainedWithOtherExtensions()
    {
        // Act
        var client = new OllamaApiClient(new Uri("http://localhost:11434"), "test-model")
            .WithQuickTimeout()
            .SetTimeout(TimeSpan.FromMinutes(7));

        // Assert
        var timeout = client.GetTimeout();
        Assert.AreEqual(TimeSpan.FromMinutes(7), timeout);
    }

    [TestMethod]
    public void BuilderMethods_OverwritePreviousTimeout()
    {
        // Arrange
        _testClient!.WithQuickTimeout(); // 2 minutes

        // Act
        _testClient.WithExtendedTimeout(); // 30 minutes

        // Assert
        var timeout = _testClient.GetTimeout();
        Assert.AreEqual(TimeSpan.FromMinutes(30), timeout);
    }

    [TestMethod]
    public void WithQuickTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => nullClient!.WithQuickTimeout());
    }

    [TestMethod]
    public void WithStandardTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => nullClient!.WithStandardTimeout());
    }

    [TestMethod]
    public void WithLongTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => nullClient!.WithLongTimeout());
    }

    [TestMethod]
    public void WithExtendedTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => nullClient!.WithExtendedTimeout());
    }

    [TestMethod]
    public void BuilderMethods_OrderOfTimeout_QuickLessThanStandardLessThanLongLessThanExtended()
    {
        // Arrange
        var client1 = new OllamaApiClient(new Uri("http://localhost:11434"), "test");
        var client2 = new OllamaApiClient(new Uri("http://localhost:11434"), "test");
        var client3 = new OllamaApiClient(new Uri("http://localhost:11434"), "test");
        var client4 = new OllamaApiClient(new Uri("http://localhost:11434"), "test");

        // Act
        client1.WithQuickTimeout();
        client2.WithStandardTimeout();
        client3.WithLongTimeout();
        client4.WithExtendedTimeout();

        // Assert
        Assert.IsTrue(client1.GetTimeout() < client2.GetTimeout());
        Assert.IsTrue(client2.GetTimeout() < client3.GetTimeout());
        Assert.IsTrue(client3.GetTimeout() < client4.GetTimeout());
    }
}
