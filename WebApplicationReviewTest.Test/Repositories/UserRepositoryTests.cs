namespace WebApplicationReviewTest.Test.Repositories;

using FluentAssertions;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Infra.Data;
using WebApplicationReviewTest.Infra.Repositories;

[TestFixture]
public class UserRepositoryTests
{
    private ApplicationDbContext _dbContext;
    private UserRepository _userRepository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _userRepository = new UserRepository(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext?.Dispose();
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User 
        { 
            Username = "testuser", 
            Email = "test@test.com", 
            Password = "password", 
            CreatedAt = DateTime.UtcNow, 
            IsActive = true 
        };
        await _userRepository.AddAsync(user);

        // Act
        var result = await _userRepository.GetByIdAsync(user.Id);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("testuser");
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Act
        var result = await _userRepository.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task GetByUsernameAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User 
        { 
            Username = "testuser", 
            Email = "test@test.com", 
            Password = "password", 
            CreatedAt = DateTime.UtcNow, 
            IsActive = true 
        };
        await _userRepository.AddAsync(user);

        // Act
        var result = await _userRepository.GetByUsernameAsync("testuser");

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be("test@test.com");
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var user1 = new User { Username = "user1", Email = "user1@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        var user2 = new User { Username = "user2", Email = "user2@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        
        await _userRepository.AddAsync(user1);
        await _userRepository.AddAsync(user2);

        // Act
        var result = await _userRepository.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
    }

    [Test]
    public async Task AddAsync_ShouldAddUser()
    {
        // Arrange
        var user = new User 
        { 
            Username = "newuser", 
            Email = "new@test.com", 
            Password = "password", 
            CreatedAt = DateTime.UtcNow, 
            IsActive = true 
        };

        // Act
        await _userRepository.AddAsync(user);
        var result = await _userRepository.GetByUsernameAsync("newuser");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateUser()
    {
        // Arrange
        var user = new User 
        { 
            Username = "testuser", 
            Email = "test@test.com", 
            Password = "password", 
            CreatedAt = DateTime.UtcNow, 
            IsActive = true 
        };
        await _userRepository.AddAsync(user);

        // Act
        user.Email = "newemail@test.com";
        await _userRepository.UpdateAsync(user);
        var updated = await _userRepository.GetByIdAsync(user.Id);

        // Assert
        updated.Email.Should().Be("newemail@test.com");
    }

    [Test]
    public async Task DeleteAsync_ShouldRemoveUser()
    {
        // Arrange
        var user = new User 
        { 
            Username = "testuser", 
            Email = "test@test.com", 
            Password = "password", 
            CreatedAt = DateTime.UtcNow, 
            IsActive = true 
        };
        await _userRepository.AddAsync(user);

        // Act
        await _userRepository.DeleteAsync(user.Id);
        var result = await _userRepository.GetByIdAsync(user.Id);

        // Assert
        result.Should().BeNull();
    }
}
