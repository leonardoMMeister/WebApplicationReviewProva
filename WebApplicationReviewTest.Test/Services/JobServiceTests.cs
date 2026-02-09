namespace WebApplicationReviewTest.Test.Services;

using FluentAssertions;
using NUnit.Framework;
using Moq;
using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Aplication.Services;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;

[TestFixture]
public class JobServiceTests
{
    private Mock<IJobRepository> _jobRepositoryMock;
    private JobService _jobService;

    [SetUp]
    public void SetUp()
    {
        _jobRepositoryMock = new Mock<IJobRepository>();
        _jobService = new JobService(_jobRepositoryMock.Object);
    }

    [Test]
    public async Task GetJobsByUserIdAsync_ShouldReturnEmptyList_WhenNoJobsExist()
    {
        // Arrange
        _jobRepositoryMock.Setup(r => r.GetByUserIdAsync(1))
            .ReturnsAsync(new List<Job>());

        // Act
        var result = await _jobService.GetJobsByUserIdAsync(1);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetJobsByUserIdAsync_ShouldReturnJobsForUser()
    {
        // Arrange
        int userId = 1;
        var jobs = new List<Job>
        {
            new Job { Id = 1, UserId = userId, Title = "Job 1", Description = "Desc 1", Status = "Pending", CreatedAt = DateTime.UtcNow },
            new Job { Id = 2, UserId = userId, Title = "Job 2", Description = "Desc 2", Status = "InProgress", CreatedAt = DateTime.UtcNow }
        };

        _jobRepositoryMock.Setup(r => r.GetByUserIdAsync(userId))
            .ReturnsAsync(jobs);

        // Act
        var result = await _jobService.GetJobsByUserIdAsync(userId);

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(j => j.UserId.Should().Be(userId));
    }

    [Test]
    public async Task CreateJobAsync_ShouldCreateJob_WithValidData()
    {
        // Arrange
        int userId = 1;
        var createJobDto = new CreateJobDto
        {
            Title = "New Job",
            Description = "Job Description",
            DueDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        var result = await _jobService.CreateJobAsync(userId, createJobDto);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("New Job");
        result.UserId.Should().Be(userId);
        result.Status.Should().Be("Pending");
        _jobRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Job>()), Times.Once);
    }

    [Test]
    public async Task UpdateJobAsync_ShouldSetCompletedAt_WhenStatusIsCompleted()
    {
        // Arrange
        int jobId = 1;
        var existingJob = new Job 
        { 
            Id = jobId, 
            UserId = 1, 
            Title = "Job", 
            Status = "Pending", 
            CreatedAt = DateTime.UtcNow 
        };

        _jobRepositoryMock.Setup(r => r.GetByIdAsync(jobId))
            .ReturnsAsync(existingJob);

        var updateJobDto = new UpdateJobDto
        {
            Title = "Updated Job",
            Status = "Completed",
            Description = "Updated Description"
        };

        // Act
        await _jobService.UpdateJobAsync(jobId, updateJobDto);

        // Assert
        existingJob.Status.Should().Be("Completed");
        existingJob.CompletedAt.Should().NotBeNull();
        _jobRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Job>()), Times.Once);
    }

    [Test]
    public async Task DeleteJobAsync_ShouldCallRepository()
    {
        // Arrange
        int jobId = 1;

        // Act
        await _jobService.DeleteJobAsync(jobId);

        // Assert
        _jobRepositoryMock.Verify(r => r.DeleteAsync(jobId), Times.Once);
    }
}
