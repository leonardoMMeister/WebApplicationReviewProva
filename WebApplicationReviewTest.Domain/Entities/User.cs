namespace WebApplicationReviewTest.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
        public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; }
    
        public ICollection<Job> Jobs { get; set; } = new List<Job>();

    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public int Idade { get; set; }
    public string Profissao { get; set; }
    public DateTime? DataNascimento { get; set; }
    public bool RecebeNotificacoes { get; set; } = true;
    public string Observacoes { get; set; }

    public bool EstaAtivo()
    {
        return IsActive;
    }

    public string ObterNomeCompleto()
    {
        return Username ?? "UsuÃ¡rio Desconhecido";
    }

    public int QuantidadeDeTarefa()
    {
        return Jobs?.Count ?? 0;
    }

    public void DesativarUsuario()
    {
        IsActive = false;
    }

    public void AtivarUsuario()
    {
        IsActive = true;
    }

    public string ObterDescricao()
    {
        return $"{Username} ({Email}) - {Profissao}";
    }
}
