namespace ContactHub_API.Domain.Entities;

public class Pessoa
{
    public int IdPessoa { get; set; }
    public string? NomePessoa { get; set; } = "";
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Whatsapp { get; set; }

    public Pessoa() { }

    public Pessoa(Pessoa pessoa)
    {
        IdPessoa = pessoa.IdPessoa;
        NomePessoa = pessoa.NomePessoa;
        Email = pessoa.Email;
        Telefone = pessoa.Telefone;
        Whatsapp = pessoa.Whatsapp;
    }

    public void Validate()
    {
        if (string.IsNullOrEmpty(NomePessoa))
        {
            throw new Exception("O campo Nome não pode estar vazio.");
        }
        if (string.IsNullOrEmpty(Email))
        {
            throw new Exception("O campo Email não pode estar vazio.");
        }
        if (string.IsNullOrEmpty(Telefone))
        {
            throw new Exception("O campo Telefone não pode estar vazio.");
        }
        if (string.IsNullOrEmpty(Whatsapp))
        {
            throw new Exception("O campo Whatsapp não pode estar vazio.");
        }
    }
}
