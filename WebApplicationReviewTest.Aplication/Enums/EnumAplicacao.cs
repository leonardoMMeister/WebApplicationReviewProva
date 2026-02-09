namespace WebApplicationReviewTest.Aplication.Enums;

/// <summary>
/// Enum para status das tarefas
/// </summary>
public enum StatusTarefa
{
    Pendente = 1,
    EmAndamento = 2,
    Concluida = 3,
    Cancelada = 4,
    Pausada = 5,
    Bloqueada = 6
}

/// <summary>
/// Enum para prioridades das tarefas
/// </summary>
public enum PrioridadeTarefa
{
    Baixa = 1,
    Media = 2,
    Alta = 3,
    Urgente = 4,
    Critica = 5
}

/// <summary>
/// Enum para estados do usuário
/// </summary>
public enum EstadoUsuario
{
    Ativo = 1,
    Inativo = 2,
    Bloqueado = 3,
    PendenteAprovacao = 4,
    Deletado = 5
}

/// <summary>
/// Enum para tipos de permissão
/// </summary>
public enum TipoPermissao
{
    Leitura = 1,
    Criacão = 2,
    Edicao = 3,
    Delecao = 4,
    Administrador = 5
}

/// <summary>
/// Enum para tipos de notificação
/// </summary>
public enum TipoNotificacao
{
    Informacao = 1,
    Aviso = 2,
    Erro = 3,
    Sucesso = 4,
    Atencao = 5
}

/// <summary>
/// Enum para categorias de tarefa
/// </summary>
public enum CategoriaTarefa
{
    Trabalho = 1,
    Pessoal = 2,
    Estudo = 3,
    Saude = 4,
    Lazer = 5,
    Financas = 6,
    Outro = 7
}

/// <summary>
/// Enum para dias da semana em português
/// </summary>
public enum DiasSemana
{
    Segunda = 1,
    Terca = 2,
    Quarta = 3,
    Quinta = 4,
    Sexta = 5,
    Sabado = 6,
    Domingo = 7
}

/// <summary>
/// Enum para meses do ano em português
/// </summary>
public enum MesesAno
{
    Janeiro = 1,
    Fevereiro = 2,
    Marco = 3,
    Abril = 4,
    Maio = 5,
    Junho = 6,
    Julho = 7,
    Agosto = 8,
    Setembro = 9,
    Outubro = 10,
    Novembro = 11,
    Dezembro = 12
}

/// <summary>
/// Enum para tipos de ação/auditoria
/// </summary>
public enum TipoAcao
{
    Visualizacao = 1,
    Criacão = 2,
    Edicao = 3,
    Delecao = 4,
    Login = 5,
    Logout = 6,
    Exportacao = 7,
    Importacao = 8
}

/// <summary>
/// Enum para resultados de operação
/// </summary>
public enum ResultadoOperacao
{
    Sucesso = 1,
    Falha = 2,
    Aviso = 3,
    Incompleto = 4,
    EstáProcessando = 5
}

/// <summary>
/// Enum para níveis de severidade
/// </summary>
public enum NivelSeveridade
{
    Baixa = 1,
    Media = 2,
    Alta = 3,
    Critica = 4,
    Catastrofica = 5
}

/// <summary>
/// Enum para tipos de usuário/papel
/// </summary>
public enum TipoUsuario
{
    Administrador = 1,
    Gerente = 2,
    Usuario = 3,
    Convidado = 4,
    Auditor = 5
}

/// <summary>
/// Extensões para facilitar conversão de enums
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Converte um enum para sua descrição em português
    /// </summary>
    public static string ObterDescricao(this Enum valor)
    {
        return valor switch
        {
            StatusTarefa.Pendente => "Pendente",
            StatusTarefa.EmAndamento => "Em Andamento",
            StatusTarefa.Concluida => "Concluída",
            StatusTarefa.Cancelada => "Cancelada",
            StatusTarefa.Pausada => "Pausada",
            StatusTarefa.Bloqueada => "Bloqueada",
            
            PrioridadeTarefa.Baixa => "Baixa",
            PrioridadeTarefa.Media => "Média",
            PrioridadeTarefa.Alta => "Alta",
            PrioridadeTarefa.Urgente => "Urgente",
            PrioridadeTarefa.Critica => "Crítica",
            
            EstadoUsuario.Ativo => "Ativo",
            EstadoUsuario.Inativo => "Inativo",
            EstadoUsuario.Bloqueado => "Bloqueado",
            EstadoUsuario.PendenteAprovacao => "Pendente Aprovação",
            EstadoUsuario.Deletado => "Deletado",
            
            DiasSemana.Segunda => "Segunda-feira",
            DiasSemana.Terca => "Terça-feira",
            DiasSemana.Quarta => "Quarta-feira",
            DiasSemana.Quinta => "Quinta-feira",
            DiasSemana.Sexta => "Sexta-feira",
            DiasSemana.Sabado => "Sábado",
            DiasSemana.Domingo => "Domingo",
            
            ResultadoOperacao.Sucesso => "Sucesso",
            ResultadoOperacao.Falha => "Falha",
            ResultadoOperacao.Aviso => "Aviso",
            ResultadoOperacao.Incompleto => "Incompleto",
            ResultadoOperacao.EstáProcessando => "Está Processando",
            
            _ => valor.ToString()
        };
    }

    /// <summary>
    /// Verifica se um enum é um valor válido
    /// </summary>
    public static bool EhValido<T>(int valor) where T : Enum
    {
        return Enum.IsDefined(typeof(T), valor);
    }

    /// <summary>
    /// Obtém todos os valores de um enum
    /// </summary>
    public static List<T> ObterTodosValores<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}
