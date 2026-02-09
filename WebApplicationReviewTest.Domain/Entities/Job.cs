namespace WebApplicationReviewTest.Domain.Entities;

public class Job
{
    public int Id { get; set; }
    public int UserId { get; set; }
        public string Title { get; set; }
    public string Description { get; set; }
    
        public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }
    
    public User User { get; set; }

    public string Prioridade { get; set; } = "MÃ©dia";
    public int PercentualConclusao { get; set; } = 0;
    public bool Arquivada { get; set; } = false;
    public string Categoria { get; set; }
    public string Responsavel { get; set; }
    public int TempoEstimadoHoras { get; set; } = 8;
    public string Observacoes { get; set; }

    public bool EstaAtrasada()
    {
        return DueDate.HasValue && DateTime.UtcNow > DueDate && Status != "Completed";
    }

    public bool EstaConcluida()
    {
        return Status == "Completed" || PercentualConclusao == 100;
    }

    public void MarcarComoConcluida()
    {
        Status = "Completed";
        CompletedAt = DateTime.UtcNow;
        PercentualConclusao = 100;
    }

    public void MarcarComoPendente()
    {
        Status = "Pending";
        CompletedAt = null;
        PercentualConclusao = 0;
    }

    public void ArquivarTarefa()
    {
        Arquivada = true;
    }

    public int TempoRestanteEmDias()
    {
        if (!DueDate.HasValue) return -1;
        return (int)(DueDate.Value - DateTime.UtcNow).TotalDays;
    }

    public string ObterStatusFormatado()
    {
        return Status switch
        {
            "Completed" => "ConcluÃ­da",
            "Pending" => "Pendente",
            "InProgress" => "Em Progresso",
            _ => "Desconhecido"
        };
    }
}
