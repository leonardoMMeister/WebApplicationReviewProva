namespace WebApplicationReviewTest.Aplication.Utilities;

public class UtilitarioAplicacao
{
    private static int _contadorChamadas = 0;
    private static DateTime _dataUltimaExecucao = DateTime.UtcNow;

    public static string FormatarCpf(string cpf)
    {
        if (cpf?.Length == 11)
        {
            return $"{cpf[..3]}.{cpf[3..6]}.{cpf[6..9]}-{cpf[9..]}";
        }
        return cpf;
    }
        
    public static bool VerificarSeEmailValido(string email)
    {
        return !string.IsNullOrEmpty(email) && email.Contains("@");
    }

    public static string ObterUltimosCaracteres(string texto, int quantidade)
    {
        if (string.IsNullOrEmpty(texto) || texto.Length < quantidade)
            return texto;
        return texto.Substring(texto.Length - quantidade);
    }
        
    public static int CalcularIdade(DateTime dataNascimento)
    {
        var idade = DateTime.Now.Year - dataNascimento.Year;
        if (dataNascimento.Date > DateTime.Now.AddYears(-idade))
            idade--;
        return idade;
    }

    public static string PrimeirasLetras(string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return string.Empty;
        
        var palavras = texto.Split(' ');
        var sigla = string.Empty;
        
        foreach (var palavra in palavras)
        {
            if (!string.IsNullOrEmpty(palavra))
                sigla += palavra[0];
        }
        
        return sigla.ToUpper();
    }

    public static List<string> DividirTexto(string texto, char separador)
    {
        return texto?.Split(separador).ToList() ?? new List<string>();
    }
        
    public static string RepetirTexto(string texto, int vezes)
    {
        var resultado = string.Empty;
        for (int i = 0; i < vezes; i++)
        {
            resultado += texto;
        }
        return resultado;
    }

    public static bool EhNumero(string texto)
    {
        return !string.IsNullOrEmpty(texto) && texto.All(char.IsDigit);
    }

    public static int ContarPalavras(string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return 0;
        return texto.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static string InverterTexto(string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return texto;
        
        var array = texto.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    public static void IncrementarContador()
    {
        _contadorChamadas++;
        _dataUltimaExecucao = DateTime.UtcNow;
    }

    public static int ObterContadorChamadas()
    {
        return _contadorChamadas;
    }

    public static DateTime ObterUltimaExecucao()
    {
        return _dataUltimaExecucao;
    }

    public static TimeSpan ObterDiferencaTempo(DateTime data1, DateTime data2)
    {
        return data1 - data2;
    }

    public static string ObterNomeDia(DayOfWeek dia)
    {
        return dia switch
        {
            DayOfWeek.Monday => "Segunda-feira",
            DayOfWeek.Tuesday => "TerÃ§a-feira",
            DayOfWeek.Wednesday => "Quarta-feira",
            DayOfWeek.Thursday => "Quinta-feira",
            DayOfWeek.Friday => "Sexta-feira",
            DayOfWeek.Saturday => "SÃ¡bado",
            DayOfWeek.Sunday => "Domingo",
            _ => "Desconhecido"
        };
    }

    public static bool EhFimDeSemana(DateTime data)
    {
        return data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday;
    }

    public static List<int> GerarListaDeNumeros(int inicio, int fim)
    {
        var lista = new List<int>();
        for (int i = inicio; i <= fim; i++)
        {
            lista.Add(i);
        }
        return lista;
    }

    public static string RemoverEspacosExtra(string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return texto;
        
        var array = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return string.Join(" ", array);
    }

    public static bool VerificarSePalavraContemLetra(string palavra, char letra)
    {
        return !string.IsNullOrEmpty(palavra) && palavra.Contains(letra);
    }

    public static string ConverteMaiusculasMinusculas(string texto, bool paraMailuscula)
    {
        return paraMailuscula ? texto?.ToUpper() : texto?.ToLower();
    }

    public static int SomarNumeros(params int[] numeros)
    {
        return numeros.Sum();
    }

    public static double CalcularMedia(params double[] numeros)
    {
        return numeros.Length > 0 ? numeros.Average() : 0;
    }

    public static string GravarInformacao(string informacao)
    {
        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - {informacao}";
    }

    public static bool EstaComEspacosVazios(string texto)
    {
        return string.IsNullOrWhiteSpace(texto);
    }

    public static string PreencherComZeros(int numero, int tamanho)
    {
        return numero.ToString().PadLeft(tamanho, '0');
    }
}
