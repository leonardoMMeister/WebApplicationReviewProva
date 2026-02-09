namespace WebApplicationReviewTest.Aplication.Constants;

public static class ConstantesAplicacao
{
    // Constantes simples
    
    public const string MENSAGEM_SUCESSO = "Operação realizada com sucesso!";
    public const string MENSAGEM_ERRO = "Ocorreu um erro ao processar a requisição";
    public const string MENSAGEM_NAO_ENCONTRADO = "Recurso não encontrado";
    public const string MENSAGEM_NAO_AUTORIZADO = "Você não tem permissão para acessar este recurso";
    
    // Mensagens de validação
    public const string VALIDACAO_CAMPO_OBRIGATORIO = "Este campo é obrigatório";
    public const string VALIDACAO_EMAIL_INVALIDO = "Email inválido";
    public const string VALIDACAO_SENHA_FRACA = "Senha deve conter pelo menos 6 caracteres";
    public const string VALIDACAO_USERNAME_CURTO = "Username deve ter pelo menos 3 caracteres";
    
    // Status de tarefa
    public const string STATUS_PENDENTE = "Pending";
    public const string STATUS_EM_ANDAMENTO = "InProgress";
    public const string STATUS_CONCLUIDA = "Completed";
    public const string STATUS_CANCELADA = "Cancelled";
    
    // Prioridades
    public const string PRIORIDADE_BAIXA = "Baixa";
    public const string PRIORIDADE_MEDIA = "Média";
    public const string PRIORIDADE_ALTA = "Alta";
    public const string PRIORIDADE_URGENTE = "Urgente";
    
    // Limites
    public const int LIMITE_MINIMO_IDADE = 18;
    public const int LIMITE_MAXIMO_IDADE = 120;
    public const int LIMITE_MINIMO_USERNAME = 3;
    public const int LIMITE_MAXIMO_USERNAME = 50;
    public const int LIMITE_MINIMO_SENHA = 6;
    public const int LIMITE_MINIMO_TITULO_TAREFA = 5;
    public const int LIMITE_MAXIMO_TITULO_TAREFA = 200;
    
    // Padrões
    public const string FORMATO_DATA_PADRAO = "dd/MM/yyyy";
    public const string FORMATO_DATA_HORA_PADRAO = "dd/MM/yyyy HH:mm:ss";
    public const string FORMATO_HORA_PADRAO = "HH:mm:ss";
    
    // Caracteres especiais
    public const char SEPARADOR_PADRAO = ',';
    public const char SEPARADOR_LINHA = ';';
    public const string CARACTERE_ESPACO = " ";
    
    // Duração
    public const int DURACAO_SESSAO_MINUTOS = 30;
    public const int DURACAO_TOKEN_HORAS = 24;
    
    // API
    public const string VERSAO_API = "1.0.0";
    public const string NOME_APLICACAO = "WebApplicationReviewTest";
    public const string DESCRICAO_APLICACAO = "Aplicação para revisão de código e análise de boas práticas";
    
    // Códigos HTTP customizados
    public const int CODIGO_SUCESSO = 200;
    public const int CODIGO_CRIADO = 201;
    public const int CODIGO_REQUISICAO_INVALIDA = 400;
    public const int CODIGO_NAO_AUTORIZADO = 401;
    public const int CODIGO_PROIBIDO = 403;
    public const int CODIGO_NAO_ENCONTRADO = 404;
    public const int CODIGO_ERRO_INTERNO = 500;
}

/// <summary>
/// Constantes para mensagens de erro detalhadas
/// </summary>
public static class MensagensErro
{
    public const string USUARIO_NAO_ENCONTRADO = "Usuário não encontrado no sistema";
    public const string TAREFA_NAO_ENCONTRADA = "Tarefa não encontrada no sistema";
    public const string EMAIL_JA_EXISTE = "Este email já está registrado";
    public const string USERNAME_JA_EXISTE = "Este username já está em uso";
    public const string SENHA_INCORRETA = "Username ou senha incorretos";
    public const string USUARIO_INATIVO = "Usuário inativo no sistema";
    public const string USUARIO_SEM_PERMISSAO = "Usuário sem permissão para esta ação";
    public const string TAREFA_VENCIDA = "Esta tarefa está vencida";
    public const string PARAMETRO_INVALIDO = "Parâmetro inválido fornecido";
    public const string BASE_DADOS_INDISPONIVEL = "Base de dados indisponível no momento";
}

/// <summary>
/// Constantes para mensagens de sucesso
/// </summary>
public static class MensagensSuccesso
{
    public const string USUARIO_CRIADO = "Usuário criado com sucesso";
    public const string USUARIO_ATUALIZADO = "Usuário atualizado com sucesso";
    public const string USUARIO_DELETADO = "Usuário deletado com sucesso";
    public const string TAREFA_CRIADA = "Tarefa criada com sucesso";
    public const string TAREFA_ATUALIZADA = "Tarefa atualizada com sucesso";
    public const string TAREFA_DELETADA = "Tarefa deletada com sucesso";
    public const string OPERACAO_SUCESSO = "Operação realizada com sucesso";
    public const string LOGIN_SUCESSO = "Login realizado com sucesso";
    public const string LOGOUT_SUCESSO = "Logout realizado com sucesso";
}

/// <summary>
/// Constantes para configurações da aplicação
/// </summary>
public static class ConfiguracaoAplicacao
{
    public const bool ATIVAR_LOGS = true;
    public const bool ATIVAR_CACHE = true;
    public const bool ATIVAR_COMPRESSAO = true;
    public const bool MODO_DESENVOLVIMENTO = true;
    public const int LIMITE_REQUISICOES_POR_MINUTO = 60;
    public const string ZONA_HORARIA_PADRAO = "America/Sao_Paulo";
}

/// <summary>
/// Constantes para a base de dados
/// </summary>
public static class ConfiguracaoBancoDados
{
    public const int TIMEOUT_CONEXAO = 30;
    public const int TAMANHO_MAXIMO_POOL = 100;
    public const int TAMANHO_MINIMO_POOL = 5;
    public const bool RETRY_AUTOMATICO = true;
    public const int NUMERO_RETENTATIVAS = 3;
}
