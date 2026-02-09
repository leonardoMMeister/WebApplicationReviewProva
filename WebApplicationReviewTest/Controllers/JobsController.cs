namespace WebApplicationReviewTest.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplicationReviewTest.Aplication.DTOs;
using WebApplicationReviewTest.Aplication.Services;

[ApiController]
[Route("api/users/{userId}/jobs")]
public class JobsController : ControllerBase
{
    private readonly JobService _jobService;
    private int _contadorTarefas = 0;
    private DateTime _ultimaRequisicao = DateTime.UtcNow;
    private string _nomeController = "JobsController";

    public JobsController(JobService jobService)
    {
        _jobService = jobService;
    }

        [HttpGet]
    public async Task<IActionResult> GetJobsByUser(int userId)
    {
        try
        {
            _contadorTarefas++;
            _ultimaRequisicao = DateTime.UtcNow;
            var dataAtual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var jobs = await _jobService.GetJobsByUserIdAsync(userId);
            return Ok(jobs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar jobs", error = ex.Message });
        }
    }

        [HttpGet("{jobId}")]
    public async Task<IActionResult> GetJob(int userId, int jobId)
    {
        try
        {
            var job = await _jobService.GetJobByIdAsync(jobId);
            
                        if (job.UserId != userId)
                return Forbid();

            return Ok(job);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = "Job nÃ£o encontrado" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob(int userId, [FromBody] CreateJobDto dto)
    {
                if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest(new { message = "TÃ­tulo Ã© obrigatÃ³rio" });
        }

        try
        {
            var job = await _jobService.CreateJobAsync(userId, dto);
            return CreatedAtAction(nameof(GetJob), new { userId, jobId = job.Id }, job);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

        [HttpPut("{jobId}")]
    public async Task<IActionResult> UpdateJob(int userId, int jobId, [FromBody] UpdateJobDto dto)
    {
        try
        {
                        await _jobService.UpdateJobAsync(jobId, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

        [HttpDelete("{jobId}")]
    public async Task<IActionResult> DeleteJob(int userId, int jobId)
    {
        try
        {
                        await _jobService.DeleteJobAsync(jobId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int ObterContadorTarefas()
    {
        return _contadorTarefas;
    }

    private DateTime ObterUltimaRequisicao()
    {
        return _ultimaRequisicao;
    }

    private string ObterNomeController()
    {
        return _nomeController;
    }

    private bool VerificarSeControllerFunciona()
    {
        return true;
    }

    private string CriarMensagemTarefa(int userId, int jobId)
    {
        return $"Tarefa {jobId} do usuÃ¡rio {userId}";
    }
}
