namespace WebApplicationReviewTest.Test.Services;

using FluentAssertions;
using NUnit.Framework;
using Moq;
using WebApplicationReviewTest.Aplication.Services;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;

[TestFixture]
public class AuthenticationServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private AuthenticationService _authenticationService;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _authenticationService = new AuthenticationService(_userRepositoryMock.Object);
    }

    [Test]
    public async Task AuthenticateAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new User 
        { 
            Id = 1, 
            Username = "testuser", 
            Email = "test@test.com", 
            Password = "password123", // TODO: ISSUE - Texto plano!
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _userRepositoryMock.Setup(r => r.GetByUsernameAsync("testuser"))
            .ReturnsAsync(user);

        // Act
        var result = await _authenticationService.AuthenticateAsync("testuser", "password123");

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task AuthenticateAsync_ShouldReturnFalse_WhenPasswordIsWrong()
    {
        // Arrange
        var user = new User 
        { 
            Id = 1, 
            Username = "testuser", 
            Email = "test@test.com", 
            Password = "password123",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _userRepositoryMock.Setup(r => r.GetByUsernameAsync("testuser"))
            .ReturnsAsync(user);

        // Act
        var result = await _authenticationService.AuthenticateAsync("testuser", "wrongpassword");

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task AuthenticateAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetByUsernameAsync("nonexistent"))
            .ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.AuthenticateAsync("nonexistent", "anypassword");

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task GetAuthenticatedUserAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User 
        { 
            Id = 1, 
            Username = "testuser", 
            Email = "test@test.com",
            Password = "password123",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _userRepositoryMock.Setup(r => r.GetByUsernameAsync("testuser"))
            .ReturnsAsync(user);

        // Act
        var result = await _authenticationService.GetAuthenticatedUserAsync("testuser");

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("testuser");
        result.Email.Should().Be("test@test.com");
    }

    [Test]
    public async Task GetAuthenticatedUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetByUsernameAsync("nonexistent"))
            .ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.GetAuthenticatedUserAsync("nonexistent");

        // Assert
        result.Should().BeNull();
    }
}
