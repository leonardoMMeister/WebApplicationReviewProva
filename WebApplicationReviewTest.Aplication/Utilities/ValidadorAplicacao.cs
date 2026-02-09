namespace WebApplicationReviewTest.Aplication.Utilities;

public class ValidadorAplicacao
{
    public static bool ValidarUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        if (username.Length < 3 || username.Length > 50)
            return false;

        return username.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-');
    }

    public static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            return false;

        if (senha.Length < 6)
            return false;

        bool temMaiuscula = senha.Any(char.IsUpper);
        bool temMinuscula = senha.Any(char.IsLower);
        bool temNumero = senha.Any(char.IsDigit);

        return temMaiuscula && temMinuscula && temNumero;
    }

    public static bool ValidarTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return false;

        string apenasNumeros = new string(telefone.Where(char.IsDigit).ToArray());
        return apenasNumeros.Length == 11;
    }

    public static bool ValidarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        string apenasNumeros = new string(cpf.Where(char.IsDigit).ToArray());
        
        if (apenasNumeros.Length != 11)
            return false;

        if (apenasNumeros.All(c => c == apenasNumeros[0]))
            return false;

        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(apenasNumeros[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(apenasNumeros[9].ToString()) != digito1)
            return false;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(apenasNumeros[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return int.Parse(apenasNumeros[10].ToString()) == digito2;
    }

    public static bool ValidarIdade(int idade)
    {
        return idade >= 18 && idade <= 120;
    }

    public static bool ValidarData(DateTime data)
    {
        return data >= DateTime.Now.AddYears(-100) && data <= DateTime.Now;
    }

    public static bool ValidarNaoNuloOuVazio(string valor)
    {
        return !string.IsNullOrWhiteSpace(valor);
    }

    public static bool ValidarComprimentoTexto(string texto, int minimo, int maximo)
    {
        if (string.IsNullOrEmpty(texto))
            return false;

        return texto.Length >= minimo && texto.Length <= maximo;
    }

    public static bool ValidarNumeroPositivo(int numero)
    {
        return numero > 0;
    }

    public static bool ValidarNumeroPositivoOuZero(int numero)
    {
        return numero >= 0;
    }

    public static bool ValidarIntervaloNumerico(int valor, int minimo, int maximo)
    {
        return valor >= minimo && valor <= maximo;
    }

    public static bool ValidarURL(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        try
        {
            var uriResult = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult2)
                && (uriResult2.Scheme == Uri.UriSchemeHttp || uriResult2.Scheme == Uri.UriSchemeHttps);
            return uriResult;
        }
        catch
        {
            return false;
        }
    }

    public static bool ValidarEndereco(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            return false;

        return endereco.Length >= 10 && endereco.Length <= 200;
    }

    public static bool ValidarCidade(string cidade)
    {
        if (string.IsNullOrWhiteSpace(cidade))
            return false;

        return cidade.Length >= 3 && cidade.Length <= 100;
    }

    public static bool ValidarEstado(string estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
            return false;

        string[] estadosValidos = 
        { 
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        return estadosValidos.Contains(estado.ToUpper());
    }

    public static bool ValidarProfissao(string profissao)
    {
        return ValidarComprimentoTexto(profissao, 3, 100);
    }

    public static bool ValidarTituloDaTarefa(string titulo)
    {
        return ValidarComprimentoTexto(titulo, 5, 200);
    }

    public static bool ValidarDescricaoTarefa(string descricao)
    {
        return ValidarComprimentoTexto(descricao, 10, 1000);
    }

    public static bool ValidarTodosCampos(string username, string email, string senha)
    {
        return ValidarUsername(username) && ValidarEmail(email) && ValidarSenha(senha);
    }

    public static List<string> ObterErrosValidacao(string username, string email, string senha)
    {
        var erros = new List<string>();

        if (!ValidarUsername(username))
            erros.Add("Username invÃ¡lido (3-50 caracteres, apenas letras, nÃºmeros, _ e -)");

        if (!ValidarEmail(email))
            erros.Add("Email invÃ¡lido");

        if (!ValidarSenha(senha))
            erros.Add("Senha fraca (mÃ­nimo 6 caracteres, com maiÃºscula, minÃºscula e nÃºmero)");

        return erros;
    }

    public static bool TemErros(List<string> erros)
    {
        return erros != null && erros.Count > 0;
    }
}
