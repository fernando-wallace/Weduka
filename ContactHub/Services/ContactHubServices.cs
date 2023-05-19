using ContactHub_API.Domain.Entities;
using Radzen;
using System.Text;
using System.Text.Json;
namespace ContactHub.Services;

public class ContactHubServices
{
    private HttpClient httpClient;
    private DialogService dialog;

    public Pessoa? usuarioAtual = new();
    public List<Pessoa>? pessoasCadastradas = new();
    public Action<bool>? LoaderStateHasChanged { get; set; }

    public ContactHubServices(IConfiguration config, DialogService dialog)
    {
        httpClient = new() { BaseAddress = new(config.GetValue<string>("UrlContactHub_API")) };
        this.dialog = dialog;
    }

    public async Task<bool> LoadPessoas()
    {
        (bool sucesso, string resposta) retorno = await ExecutaRequisicao("Pessoa/GetPessoas", HttpMethod.Get);
        if (retorno.sucesso)
        {
            pessoasCadastradas = JsonSerializer.Deserialize<List<Pessoa>>(retorno.resposta) ?? new();
        }
        return retorno.sucesso;
    }

    public async Task<(bool, string)> ExecutaRequisicao(string endpoint, HttpMethod metodo, string? content = null, bool exibirRetorno = false)
    {
        try
        {
            HttpRequestMessage request = new(metodo, endpoint);
            if (metodo != HttpMethod.Get && !string.IsNullOrEmpty(content))
            {
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();

            LoaderStateHasChanged?.Invoke(false);
            if (exibirRetorno)
            {
                await dialog.Alert(responseContent, response.IsSuccessStatusCode ? "Sucesso" : "Erro");
            }

            return new(response.IsSuccessStatusCode, responseContent);
        }
        catch (Exception ex)
        {
            LoaderStateHasChanged?.Invoke(false);
            await dialog.Alert(
                ex.Message.Contains("Failed to fetch") ? "A API do Sistema está indisponível no momento. Verifique sua conexão ou tente novamente mais tarde." : ex.Message,
                "Erro de comunicação com API"
                );
            return new(false, ex.Message);
        }
    }
}
