namespace WebApplicationReviewTest.Infra.Repositories;

using Microsoft.EntityFrameworkCore;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;
using WebApplicationReviewTest.Infra.Data;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;

    public JobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Job> GetByIdAsync(int id)
    {
        return await _context.Jobs.FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<List<Job>> GetByUserIdAsync(int userId)
    {
        return await _context.Jobs
            .Where(j => j.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Job>> GetAllAsync()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task AddAsync(Job job)
    {
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Job job)
    {
        _context.Jobs.Update(job);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int jobId)
    {
        var job = await GetByIdAsync(jobId);
        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
    }
}
