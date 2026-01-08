# Contributing to ElBruno.OllamaSharp.Extensions

Thank you for your interest in contributing! This document provides guidelines for contributing to the project.

## 🎯 Ways to Contribute

- **Bug Reports**: Submit detailed bug reports with reproduction steps
- **Feature Requests**: Propose new features or improvements
- **Code Contributions**: Submit pull requests for bug fixes or new features
- **Documentation**: Improve or expand documentation
- **Testing**: Add or improve unit tests
- **Examples**: Create sample applications or tutorials

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK (preview)
- Git
- Your favorite IDE (Visual Studio, VS Code, or Rider)

### Setup Development Environment

1. **Fork and Clone**

   ```bash
   git fork https://github.com/elbruno/elbruno.OllamaSharp.Extensions
   cd elbruno.OllamaSharp.Extensions
   ```

2. **Build the Project**

   ```bash
   dotnet build ElBruno.OllamaSharp.Extensions.sln
   ```

3. **Run Tests**

   ```bash
   dotnet test ElBruno.OllamaSharp.Extensions.Tests/ElBruno.OllamaSharp.Extensions.Tests.csproj
   ```

## 📝 Code Style Guidelines

### C# Coding Standards

- Follow Microsoft's [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use C# 14 preview features where appropriate
- Enable nullable reference types
- Use expression-bodied members for simple methods
- Prefer `var` for local variables when type is obvious

### Naming Conventions

- Classes: `PascalCase`
- Methods: `PascalCase`
- Parameters: `camelCase`
- Private fields: `_camelCase` with underscore prefix
- Constants: `PascalCase`

### Documentation

- Add XML documentation comments for all public APIs
- Include `<summary>`, `<param>`, `<returns>`, and `<example>` tags
- Provide meaningful examples in documentation

Example:

```csharp
/// <summary>
/// Sets the timeout for the OllamaApiClient's underlying HttpClient.
/// </summary>
/// <param name="client">The OllamaApiClient instance.</param>
/// <param name="timeout">The timeout duration to set.</param>
/// <returns>The same OllamaApiClient instance for method chaining.</returns>
/// <example>
/// <code>
/// var client = new OllamaApiClient(uri, model)
///     .SetTimeout(TimeSpan.FromMinutes(10));
/// </code>
/// </example>
public static OllamaApiClient SetTimeout(this OllamaApiClient client, TimeSpan timeout)
{
    // Implementation
}
```

## 🧪 Testing Guidelines

### Unit Tests

- Write comprehensive unit tests for all new features
- Use MSTest 4.0 framework
- Follow Arrange-Act-Assert pattern
- Test both success and failure scenarios
- Include edge cases and boundary conditions

### Test Naming

```csharp
[TestMethod]
public void MethodName_Scenario_ExpectedBehavior()
{
    // Test implementation
}
```

Example:

```csharp
[TestMethod]
public void SetTimeout_WithValidTimeout_SetsTimeoutSuccessfully()
{
    // Arrange
    var client = new OllamaApiClient(uri, model);
    var timeout = TimeSpan.FromMinutes(5);

    // Act
    client.SetTimeout(timeout);

    // Assert
    Assert.AreEqual(timeout, client.GetTimeout());
}
```

### Test Coverage

- Aim for >80% code coverage
- All public methods must have tests
- Test null safety with `ArgumentNullException` tests
- Test parameter validation

## 📦 Pull Request Process

### Before Submitting

1. Ensure all tests pass
2. Add tests for new functionality
3. Update documentation
4. Follow code style guidelines
5. Update CHANGELOG.md with your changes

### PR Guidelines

1. **Create a feature branch**

   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes**
   - Write clean, documented code
   - Add tests
   - Update documentation

3. **Commit with meaningful messages**

   ```bash
   git commit -m "Add: Timeout retry policy extension"
   ```

   Use prefixes:
   - `Add:` for new features
   - `Fix:` for bug fixes
   - `Update:` for updates to existing features
   - `Docs:` for documentation changes
   - `Test:` for test-related changes

4. **Push and create PR**

   ```bash
   git push origin feature/your-feature-name
   ```

5. **PR Template**

   ```markdown
   ## Description
   Brief description of changes

   ## Type of Change
   - [ ] Bug fix
   - [ ] New feature
   - [ ] Breaking change
   - [ ] Documentation update

   ## Testing
   - [ ] All existing tests pass
   - [ ] New tests added
   - [ ] Manual testing completed

   ## Checklist
   - [ ] Code follows style guidelines
   - [ ] Self-review completed
   - [ ] Documentation updated
   - [ ] CHANGELOG.md updated
   ```

## 🐛 Bug Reports

### Bug Report Template

```markdown
**Describe the bug**
A clear description of the bug

**To Reproduce**
Steps to reproduce:
1. Step 1
2. Step 2
3. See error

**Expected behavior**
What you expected to happen

**Actual behavior**
What actually happened

**Environment**
- OS: [e.g., Windows 11]
- .NET Version: [e.g., 10.0.101]
- Package Version: [e.g., 1.0.0]

**Additional context**
Any other relevant information
```

## 💡 Feature Requests

### Feature Request Template

```markdown
**Is your feature request related to a problem?**
Description of the problem

**Proposed solution**
How would you solve it?

**Alternatives considered**
Other approaches you've thought about

**Additional context**
Any other relevant information
```

## 📚 Documentation Guidelines

### Documentation Structure

- **README.md**: Overview and quick start
- **QUICKSTART.md**: 5-minute getting started guide
- **IMPLEMENTATION.md**: Technical deep-dive
- **CSHARP14.md**: C# 14 features explanation
- **IMPROVEMENTS.md**: Future roadmap
- **API docs**: XML comments in code

### Writing Style

- Be clear and concise
- Use code examples
- Include prerequisites
- Add troubleshooting sections
- Keep it up-to-date

## 🔒 Security

### Reporting Security Issues

- **Do NOT** create public issues for security vulnerabilities
- Email security concerns to: [security contact]
- Include detailed reproduction steps
- Allow time for fix before disclosure

## 📜 License

By contributing, you agree that your contributions will be licensed under the MIT License.

## 🙏 Recognition

Contributors will be recognized in:

- CHANGELOG.md for each release
- README.md contributors section (if added)
- Release notes

## 📞 Questions?

- Create a discussion in GitHub Discussions
- Open an issue with the `question` label
- Contact maintainer: Bruno Capuano

## 🎉 Thank You

Your contributions make this project better for everyone!

---

**Maintainer:** Bruno Capuano  
**Repository:** <https://github.com/elbruno/elbruno.OllamaSharp.Extensions>  
**License:** MIT
