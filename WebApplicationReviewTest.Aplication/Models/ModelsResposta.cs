namespace WebApplicationReviewTest.Aplication.Models;

/// <summary>
/// Modelo padrão de resposta da API
/// </summary>
public class RespostaApi<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public T Dados { get; set; }
    public List<string> Erros { get; set; } = new List<string>();
    public DateTime DataRequisicao { get; set; } = DateTime.UtcNow;
    public string CodigoReferencia { get; set; }
    public int CodigoHttp { get; set; }

    public RespostaApi()
    {
    }

    public RespostaApi(bool sucesso, string mensagem, T dados)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        Dados = dados;
    }

    public static RespostaApi<T> CriarSucesso(T dados, string mensagem = "Operação realizada com sucesso")
    {
        return new RespostaApi<T>
        {
            Sucesso = true,
            Mensagem = mensagem,
            Dados = dados,
            CodigoHttp = 200
        };
    }

    public static RespostaApi<T> CriarErro(string mensagem, List<string> erros = null)
    {
        return new RespostaApi<T>
        {
            Sucesso = false,
            Mensagem = mensagem,
            Erros = erros ?? new List<string>(),
            CodigoHttp = 400
        };
    }

    public static RespostaApi<T> CriarErroInterno(string mensagem = "Erro interno do servidor")
    {
        return new RespostaApi<T>
        {
            Sucesso = false,
            Mensagem = mensagem,
            CodigoHttp = 500
        };
    }
}

/// <summary>
/// Modelo para resposta paginada
/// </summary>
public class RespostaPaginada<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public List<T> Dados { get; set; } = new List<T>();
    public int TotalRegistros { get; set; }
    public int PaginaAtual { get; set; }
    public int TamanhoPagina { get; set; }
    public int TotalPaginas { get; set; }
    public bool TemProxima { get; set; }
    public bool TemAnterior { get; set; }
    public DateTime DataRequisicao { get; set; } = DateTime.UtcNow;

    public void ConfigurarPaginacao(int totalRegistros, int paginaAtual, int tamanhoPagina)
    {
        TotalRegistros = totalRegistros;
        PaginaAtual = paginaAtual;
        TamanhoPagina = tamanhoPagina;
        TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanhoPagina);
        TemProxima = paginaAtual < TotalPaginas;
        TemAnterior = paginaAtual > 1;
    }
}

/// <summary>
/// Modelo para erros de validação
/// </summary>
public class RespostaValidacao
{
    public bool Sucesso { get; set; } = false;
    public string Mensagem { get; set; } = "Erro de validação";
    public Dictionary<string, List<string>> Erros { get; set; } = new Dictionary<string, List<string>>();

    public void AdicionarErro(string campo, string erro)
    {
        if (!Erros.ContainsKey(campo))
        {
            Erros[campo] = new List<string>();
        }
        Erros[campo].Add(erro);
    }

    public bool TemErros()
    {
        return Erros.Count > 0;
    }
}

/// <summary>
/// Modelo para resposta de autenticação
/// </summary>
public class RespostaAutenticacao
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime DataExpiracao { get; set; }
    public object Usuario { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}

/// <summary>
/// Modelo para resultado de operação
/// </summary>
public class ResultadoOperacao
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public object Dados { get; set; }
    public List<string> Aviso { get; set; } = new List<string>();
    public TimeSpan TempoExecucao { get; set; }

    public static ResultadoOperacao CriarSucesso(object dados, string mensagem)
    {
        return new ResultadoOperacao
        {
            Sucesso = true,
            Mensagem = mensagem,
            Dados = dados
        };
    }

    public static ResultadoOperacao CriarFalha(string mensagem)
    {
        return new ResultadoOperacao
        {
            Sucesso = false,
            Mensagem = mensagem
        };
    }
}

/// <summary>
/// Modelo para metadados da resposta
/// </summary>
public class Metadados
{
    public string Versao { get; set; } = "1.0.0";
    public string Ambiente { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
    public string UsuarioId { get; set; }
    public string CodigoReferencia { get; set; }
    public string IPCliente { get; set; }
    public string Navegador { get; set; }
    public TimeSpan TempoResposta { get; set; }
}

/// <summary>
/// Modelo para resposta com metadados
/// </summary>
public class RespostaComMetadados<T>
{
    public RespostaApi<T> Resposta { get; set; }
    public Metadados Metadados { get; set; }

    public RespostaComMetadados(RespostaApi<T> resposta, Metadados metadados)
    {
        Resposta = resposta;
        Metadados = metadados;
    }
}

/// <summary>
/// Modelo para relatório de operações
/// </summary>
public class RelatoriOperacao
{
    public string Titulo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int TotalOperacoes { get; set; }
    public int OperacoesBemSucedidas { get; set; }
    public int Operacoesfalhadas { get; set; }
    public List<string> Erros { get; set; } = new List<string>();
    public string Resumo { get; set; }

    public void CalcularResumo()
    {
        var duracao = DataFim - DataInicio;
        var percentualSucesso = TotalOperacoes > 0 ? (OperacoesBemSucedidas * 100) / TotalOperacoes : 0;
        Resumo = $"{OperacoesBemSucedidas}/{TotalOperacoes} operações bem-sucedidas ({percentualSucesso}%). Duração: {duracao.TotalSeconds} segundos";
    }
}

/// <summary>
/// Modelo para resposta em lote
/// </summary>
public class RespostaLote<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public List<T> Itens { get; set; } = new List<T>();
    public List<string> ItensComErro { get; set; } = new List<string>();
    public int TotalProcessado { get; set; }
    public int TotalComErro { get; set; }
}

/// <summary>
/// Modelo para componente de mensagem
/// </summary>
public class ComponenteMensagem
{
    public string Tipo { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public string Icone { get; set; }
    public string Cor { get; set; }
    public bool Dismissivel { get; set; } = true;
    public DateTime DataCricacao { get; set; } = DateTime.UtcNow;
}
