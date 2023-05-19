using ContactHub_API.Domain.Entities;
using System.Text;
using System.Text.Json;

namespace ContactHub_API.Infrastructure.Contexts;

public class JsonFileContext
{
    public List<Pessoa> DbSetPessoas = new();
    public List<LinkPessoa> DbSetLinksPessoas = new();

    public JsonFileContext()
    {
        LoadData();
    }

    private void LoadData()
    {
        string filePath = "./Database/Pessoas.json";

        if (File.Exists(filePath))
        {
            string conteudoArquivoDecodificado = ObterConteudoArquivoDecodificado(filePath);
            DbSetPessoas = !string.IsNullOrEmpty(conteudoArquivoDecodificado) ?
                JsonSerializer.Deserialize<List<Pessoa>>(conteudoArquivoDecodificado) ?? new() :
                new();
        }

        filePath = "./Database/LinksPessoas.json";

        if (File.Exists(filePath))
        {
            string conteudoArquivoDecodificado = ObterConteudoArquivoDecodificado(filePath);
            DbSetLinksPessoas = !string.IsNullOrEmpty(conteudoArquivoDecodificado) ?
                JsonSerializer.Deserialize<List<LinkPessoa>>(conteudoArquivoDecodificado) ?? new() :
                new();
        }
    }

    public void SaveChanges()
    {
        File.WriteAllText("./Database/Pessoas.json", JsonSerializer.Serialize(DbSetPessoas), Encoding.UTF8);
        File.WriteAllText("./Database/LinksPessoas.json", JsonSerializer.Serialize(DbSetLinksPessoas), Encoding.UTF8);
    }

    private static string ObterConteudoArquivoDecodificado(string filePath)
    {
        string conteudoArquivoBruto = File.ReadAllText(filePath, Encoding.UTF8);
        return System.Text.RegularExpressions.Regex.Unescape(conteudoArquivoBruto);
    }
}
