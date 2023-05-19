using ContactHub_API.Domain.DTOs;
using ContactHub_API.Domain.Entities;
using ContactHub_API.Infrastructure.Contexts;

namespace ContactHub_API.Infrastructure.Repositories;

public class APIContatosRepository : _Interface_APIContatosRepository
{
    private readonly JsonFileContext _context;

    public APIContatosRepository()
    {
        _context = new();
    }

    #region Pessoa
    public Pessoa GetPessoa(int idPessoa)
    {
        Pessoa? pessoa = _context.DbSetPessoas.FirstOrDefault(p => p.IdPessoa == idPessoa);
        if (pessoa is null)
        {
            throw new Exception($"Não existe Pessoa cadastrada para o IdPessoa [{idPessoa}].");
        }
        return pessoa;
    }

    public List<Pessoa> GetPessoas() => _context.DbSetPessoas.ToList();

    public string CreatePessoa(Pessoa pessoa, out int idPessoaCriada)
    {
        pessoa.IdPessoa = _context.DbSetPessoas.Any() ? _context.DbSetPessoas.Max(p => p.IdPessoa) + 1 : 1;
        _context.DbSetPessoas.Add(pessoa);
        _context.SaveChanges();

        idPessoaCriada = pessoa.IdPessoa;

        return "Cadastro realizado com sucesso.";
    }

    public string UpdatePessoa(Pessoa pessoa)
    {
        Pessoa pessoaBanco = GetPessoa(pessoa.IdPessoa);

        pessoaBanco.NomePessoa = pessoa.NomePessoa;
        pessoaBanco.Email = pessoa.Email;
        pessoaBanco.Telefone = pessoa.Telefone;
        pessoaBanco.Whatsapp = pessoa.Whatsapp;

        _context.SaveChanges();

        return "Cadastro atualizado com sucesso.";
    }

    public string DeletePessoa(int IdPessoa)
    {
        Pessoa pessoaBanco = GetPessoa(IdPessoa);
        _context.DbSetPessoas.Remove(pessoaBanco);
        _context.DbSetLinksPessoas.RemoveAll(l => l.IdPessoa == IdPessoa || l.IdPessoaLink == IdPessoa);
        _context.SaveChanges();

        return "Cadastro deletado com sucesso.";
    }
    #endregion

    #region LinksPessoa
    public List<LinkPessoa> GetLinksPessoa(int idPessoa)
    {
        Pessoa pessoaBanco = GetPessoa(idPessoa);
        return _context.DbSetLinksPessoas.Where(l => l.IdPessoa == pessoaBanco.IdPessoa).ToList();
    }

    public string CreateLinkPessoa(CreateLinkPessoaRequestDTO linkPessoaResquestDTO)
    {
        Pessoa pessoaSolicitanteBanco = GetPessoa(linkPessoaResquestDTO.IdPessoaSolicitante);
        int idPessoaContato = linkPessoaResquestDTO.Pessoa?.IdPessoa ?? 0;

        //Se o contato ainda não tiver IdPessoa, quer dizer que deve ser criada uma nova Pessoa.
        if (linkPessoaResquestDTO.Pessoa?.IdPessoa == 0)
        {
            CreatePessoa(linkPessoaResquestDTO.Pessoa, out idPessoaContato);
        }

        LinkPessoa novoLinkPessoa = new()
        {
            IdPessoa = pessoaSolicitanteBanco.IdPessoa,
            IdPessoaLink = idPessoaContato
        };

        _context.DbSetLinksPessoas.Add(novoLinkPessoa);
        _context.SaveChanges();

        return "Contato vinculado com sucesso.";
    }

    public string DeleteLinkPessoa(LinkPessoa linkPessoa)
    {
        _context.DbSetLinksPessoas.RemoveAll(l => l.IdPessoa == linkPessoa.IdPessoa && l.IdPessoaLink == linkPessoa.IdPessoaLink);
        _context.SaveChanges();

        return "Contato desvinculado com sucesso.";
    }

    public string DeleteLinksPessoa(int idPessoa)
    {
        _context.DbSetLinksPessoas.RemoveAll(l => l.IdPessoa == idPessoa);
        _context.SaveChanges();

        return "Contatos desvinculados com sucesso.";
    }
    #endregion
}
