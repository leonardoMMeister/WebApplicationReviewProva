namespace WebApplicationReviewTest.Aplication.Services;

using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Domain.Entities;
using WebApplicationReviewTest.Domain.Interfaces;

public class JobService
{
    private readonly IJobRepository _jobRepository;
    private int _totalTarefasProcessadas = 0;
    private DateTime _ultimaAtualizacao = DateTime.UtcNow;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<List<JobDto>> GetJobsByUserIdAsync(int userId)
    {
                var jobs = await _jobRepository.GetByUserIdAsync(userId);
        return jobs.Select(j => MapToDto(j)).ToList();
    }

    public async Task<JobDto> GetJobByIdAsync(int id)
    {
        var job = await _jobRepository.GetByIdAsync(id);
        return MapToDto(job);
    }

    public async Task<JobDto> CreateJobAsync(int userId, CreateJobDto dto)
    {
                var job = new Job
        {
            UserId = userId,
            Title = dto.Title,
            Description = dto.Description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            DueDate = dto.DueDate
        };

        await _jobRepository.AddAsync(job);
        
                return MapToDto(job);
    }

    public async Task UpdateJobAsync(int jobId, UpdateJobDto dto)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        
                job.Title = dto.Title;
        job.Description = dto.Description;
        job.Status = dto.Status;
        job.DueDate = dto.DueDate;

        if (dto.Status == "Completed")
            job.CompletedAt = DateTime.UtcNow;

        await _jobRepository.UpdateAsync(job);
    }

    public async Task DeleteJobAsync(int jobId)
    {
        await _jobRepository.DeleteAsync(jobId);
    }

    public int ObterTotalTarefasProcessadas()
    {
        return _totalTarefasProcessadas;
    }

    public DateTime ObterUltimaAtualizacao()
    {
        return _ultimaAtualizacao;
    }

    public void IncrementarTotalTarefas()
    {
        _totalTarefasProcessadas++;
        _ultimaAtualizacao = DateTime.UtcNow;
    }

    public string ObterStatusEmPortugues(string status)
    {
        return status switch
        {
            "Pending" => "Pendente",
            "InProgress" => "Em Andamento",
            "Completed" => "ConcluÃ­da",
            "Cancelled" => "Cancelada",
            _ => "Desconhecido"
        };
    }

    public bool VerificarSeTarefaVenceu(DateTime? dueDate)
    {
        return dueDate.HasValue && DateTime.UtcNow > dueDate;
    }

    private JobDto MapToDto(Job job)
    {
        return new JobDto
        {
            Id = job.Id,
            UserId = job.UserId,
            Title = job.Title,
            Description = job.Description,
            Status = job.Status,
            CreatedAt = job.CreatedAt,
            CompletedAt = job.CompletedAt,
            DueDate = job.DueDate
        };
    }
}
