namespace WebApplicationReviewTest.Test.Repositories;

using FluentAssertions;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Infra.Data;
using WebApplicationReviewTest.Infra.Repositories;

[TestFixture]
public class JobRepositoryTests
{
    private ApplicationDbContext _dbContext;
    private JobRepository _jobRepository;
    private UserRepository _userRepository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _jobRepository = new JobRepository(_dbContext);
        _userRepository = new UserRepository(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext?.Dispose();
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnJob_WhenJobExists()
    {
        // Arrange
        var user = new User { Username = "testuser", Email = "test@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        await _userRepository.AddAsync(user);

        var job = new Job { UserId = user.Id, Title = "Test Job", Description = "Test", Status = "Pending", CreatedAt = DateTime.UtcNow };
        await _jobRepository.AddAsync(job);

        // Act
        var result = await _jobRepository.GetByIdAsync(job.Id);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Job");
    }

    [Test]
    public async Task GetByUserIdAsync_ShouldReturnJobsForUser()
    {
        // Arrange
        var user = new User { Username = "testuser", Email = "test@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        await _userRepository.AddAsync(user);

        var job1 = new Job { UserId = user.Id, Title = "Job 1", Description = "Desc 1", Status = "Pending", CreatedAt = DateTime.UtcNow };
        var job2 = new Job { UserId = user.Id, Title = "Job 2", Description = "Desc 2", Status = "Pending", CreatedAt = DateTime.UtcNow };
        
        await _jobRepository.AddAsync(job1);
        await _jobRepository.AddAsync(job2);

        // Act
        var result = await _jobRepository.GetByUserIdAsync(user.Id);

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(j => j.UserId.Should().Be(user.Id));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllJobs()
    {
        // Arrange
        var user = new User { Username = "testuser", Email = "test@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        await _userRepository.AddAsync(user);

        var job1 = new Job { UserId = user.Id, Title = "Job 1", Description = "Desc 1", Status = "Pending", CreatedAt = DateTime.UtcNow };
        var job2 = new Job { UserId = user.Id, Title = "Job 2", Description = "Desc 2", Status = "Pending", CreatedAt = DateTime.UtcNow };
        
        await _jobRepository.AddAsync(job1);
        await _jobRepository.AddAsync(job2);

        // Act
        var result = await _jobRepository.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
    }

    [Test]
    public async Task AddAsync_ShouldAddJob()
    {
        // Arrange
        var user = new User { Username = "testuser", Email = "test@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        await _userRepository.AddAsync(user);

        var job = new Job { UserId = user.Id, Title = "New Job", Description = "New Description", Status = "Pending", CreatedAt = DateTime.UtcNow };

        // Act
        await _jobRepository.AddAsync(job);
        var result = await _jobRepository.GetByIdAsync(job.Id);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("New Job");
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateJob()
    {
        // Arrange
        var user = new User { Username = "testuser", Email = "test@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        await _userRepository.AddAsync(user);

        var job = new Job { UserId = user.Id, Title = "Original Title", Description = "Desc", Status = "Pending", CreatedAt = DateTime.UtcNow };
        await _jobRepository.AddAsync(job);

        // Act
        job.Title = "Updated Title";
        await _jobRepository.UpdateAsync(job);
        var updated = await _jobRepository.GetByIdAsync(job.Id);

        // Assert
        updated.Title.Should().Be("Updated Title");
    }

    [Test]
    public async Task DeleteAsync_ShouldRemoveJob()
    {
        // Arrange
        var user = new User { Username = "testuser", Email = "test@test.com", Password = "pass", CreatedAt = DateTime.UtcNow, IsActive = true };
        await _userRepository.AddAsync(user);

        var job = new Job { UserId = user.Id, Title = "Job to Delete", Description = "Desc", Status = "Pending", CreatedAt = DateTime.UtcNow };
        await _jobRepository.AddAsync(job);

        // Act
        await _jobRepository.DeleteAsync(job.Id);
        var result = await _jobRepository.GetByIdAsync(job.Id);

        // Assert
        result.Should().BeNull();
    }
}
