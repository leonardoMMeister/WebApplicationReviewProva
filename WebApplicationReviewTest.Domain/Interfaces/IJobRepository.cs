namespace WebApplicationReviewTest.Domain.Interfaces;

using WebApplicationReviewTest.Domain.Entities;

public interface IJobRepository
{
    Task<Job> GetByIdAsync(int id);
    Task<List<Job>> GetByUserIdAsync(int userId);
    Task<List<Job>> GetAllAsync();
    Task AddAsync(Job job);
    Task UpdateAsync(Job job);
    Task DeleteAsync(int jobId);
}
