namespace WebApplicationReviewTest.Test.Services;

using FluentAssertions;
using NUnit.Framework;
using Moq;
using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Aplication.Services;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Test]
    public async Task GetAllUsersAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<User>());

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        result.Should().BeEmpty();
        _userRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetAllUsersAsync_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "user1", Email = "user1@test.com", Password = "pass123", CreatedAt = DateTime.UtcNow, IsActive = true },
            new User { Id = 2, Username = "user2", Email = "user2@test.com", Password = "pass456", CreatedAt = DateTime.UtcNow, IsActive = true }
        };

        _userRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        result.Should().HaveCount(2);
        result[0].Username.Should().Be("user1");
        result[1].Username.Should().Be("user2");
    }

    [Test]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
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

        _userRepositoryMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("testuser");
        result.Email.Should().Be("test@test.com");
    }

    [Test]
    public async Task CreateUserAsync_ShouldCreateUser_WithValidData()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            Username = "newuser",
            Email = "newuser@test.com",
            Password = "password123"
        };

        // Act
        var result = await _userService.CreateUserAsync(createUserDto);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("newuser");
        result.Email.Should().Be("newuser@test.com");
        result.IsActive.Should().BeTrue();
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_ShouldCallRepository_WhenUserIdIsProvided()
    {
        // Arrange
        int userId = 1;

        // Act
        await _userService.DeleteUserAsync(userId);

        // Assert
        _userRepositoryMock.Verify(r => r.DeleteAsync(userId), Times.Once);
    }
}
