using Microsoft.VisualStudio.TestTools.UnitTesting;
using OllamaSharp;
using ElBruno.OllamaSharp.Extensions;

namespace ElBruno.OllamaSharp.Extensions.Tests;

[TestClass]
public class OllamaApiClientExtensionsTests
{
    private OllamaApiClient? _testClient;

    [TestInitialize]
    public void Setup()
    {
        _testClient = new OllamaApiClient(new Uri("http://localhost:11434"), "test-model");
    }

    [TestMethod]
    public void SetTimeout_WithValidTimeout_SetsTimeoutSuccessfully()
    {
        // Arrange
        var timeout = TimeSpan.FromMinutes(5);

        // Act
        var result = _testClient!.SetTimeout(timeout);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_testClient, result);
        var actualTimeout = _testClient.GetTimeout();
        Assert.IsNotNull(actualTimeout);
        Assert.AreEqual(timeout, actualTimeout.Value);
    }

    [TestMethod]
    public void SetTimeout_WithFluentChaining_ReturnsClientInstance()
    {
        // Arrange
        var timeout = TimeSpan.FromMinutes(10);

        // Act
        var result = _testClient!.SetTimeout(timeout);

        // Assert
        Assert.AreSame(_testClient, result);
    }

    [TestMethod]
    public void SetTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => 
            nullClient!.SetTimeout(TimeSpan.FromMinutes(5)));
    }

    [TestMethod]
    public void SetTimeout_WithZeroTimeout_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => 
            _testClient!.SetTimeout(TimeSpan.Zero));
    }

    [TestMethod]
    public void SetTimeout_WithNegativeTimeout_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => 
            _testClient!.SetTimeout(TimeSpan.FromSeconds(-1)));
    }

    [TestMethod]
    public void GetTimeout_AfterSetTimeout_ReturnsCorrectTimeout()
    {
        // Arrange
        var expectedTimeout = TimeSpan.FromMinutes(7);
        _testClient!.SetTimeout(expectedTimeout);

        // Act
        var actualTimeout = _testClient.GetTimeout();

        // Assert
        Assert.IsNotNull(actualTimeout);
        Assert.AreEqual(expectedTimeout, actualTimeout.Value);
    }

    [TestMethod]
    public void GetTimeout_OnNewClient_ReturnsDefaultTimeout()
    {
        // Act
        var timeout = _testClient!.GetTimeout();

        // Assert
        Assert.IsNotNull(timeout);
        Assert.IsTrue(timeout.Value.TotalSeconds > 0);
    }

    [TestMethod]
    public void GetTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => nullClient!.GetTimeout());
    }

    [TestMethod]
    public void ConfigureTimeout_WithValidFunction_ConfiguresTimeout()
    {
        // Arrange
        var initialTimeout = TimeSpan.FromMinutes(2);
        _testClient!.SetTimeout(initialTimeout);

        // Act
        var result = _testClient.ConfigureTimeout(current => 
            current.HasValue ? current.Value * 2 : TimeSpan.FromMinutes(5));

        // Assert
        Assert.AreEqual(_testClient, result);
        var newTimeout = _testClient.GetTimeout();
        Assert.IsNotNull(newTimeout);
        Assert.AreEqual(TimeSpan.FromMinutes(4), newTimeout.Value);
    }

    [TestMethod]
    public void ConfigureTimeout_WithNullCurrentTimeout_UsesDefaultValue()
    {
        // Act
        var result = _testClient!.ConfigureTimeout(current => 
            current ?? TimeSpan.FromMinutes(5));

        // Assert
        Assert.IsNotNull(result);
        var timeout = _testClient.GetTimeout();
        Assert.IsNotNull(timeout);
    }

    [TestMethod]
    public void ConfigureTimeout_WithNullClient_ThrowsArgumentNullException()
    {
        // Arrange
        OllamaApiClient? nullClient = null;

        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => 
            nullClient!.ConfigureTimeout(current => TimeSpan.FromMinutes(5)));
    }

    [TestMethod]
    public void ConfigureTimeout_WithNullConfigureFunction_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.ThrowsExactly<ArgumentNullException>(() => 
            _testClient!.ConfigureTimeout(null!));
    }

    [TestMethod]
    public void SetTimeout_MultipleTimesCalled_UpdatesTimeoutEachTime()
    {
        // Arrange & Act
        _testClient!.SetTimeout(TimeSpan.FromMinutes(2));
        var timeout1 = _testClient.GetTimeout();

        _testClient.SetTimeout(TimeSpan.FromMinutes(5));
        var timeout2 = _testClient.GetTimeout();

        _testClient.SetTimeout(TimeSpan.FromMinutes(10));
        var timeout3 = _testClient.GetTimeout();

        // Assert
        Assert.AreEqual(TimeSpan.FromMinutes(2), timeout1);
        Assert.AreEqual(TimeSpan.FromMinutes(5), timeout2);
        Assert.AreEqual(TimeSpan.FromMinutes(10), timeout3);
    }

    [TestMethod]
    public void SetTimeout_WithVeryLargeTimeout_SetsTimeoutSuccessfully()
    {
        // Arrange
        var largeTimeout = TimeSpan.FromHours(24);

        // Act
        _testClient!.SetTimeout(largeTimeout);
        var actualTimeout = _testClient.GetTimeout();

        // Assert
        Assert.IsNotNull(actualTimeout);
        Assert.AreEqual(largeTimeout, actualTimeout.Value);
    }

    [TestMethod]
    public void SetTimeout_WithMinimalTimeout_SetsTimeoutSuccessfully()
    {
        // Arrange
        var minimalTimeout = TimeSpan.FromMilliseconds(1);

        // Act
        _testClient!.SetTimeout(minimalTimeout);
        var actualTimeout = _testClient.GetTimeout();

        // Assert
        Assert.IsNotNull(actualTimeout);
        Assert.AreEqual(minimalTimeout, actualTimeout.Value);
    }
}
