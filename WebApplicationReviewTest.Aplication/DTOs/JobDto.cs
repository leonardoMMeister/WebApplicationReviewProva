namespace WebApplicationReviewTest.Aplication.DTOs;

public class JobDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string Prioridade { get; set; }
    public int PercentualConclusao { get; set; }
    public string Categoria { get; set; }
}

public class CreateJobDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string Prioridade { get; set; }
    public string Categoria { get; set; }
    public int TempoEstimadoHoras { get; set; }
}

public class UpdateJobDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public string Prioridade { get; set; }
    public int PercentualConclusao { get; set; }
}
