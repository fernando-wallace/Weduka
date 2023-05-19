using ContactHub_API.Domain.DTOs;
using ContactHub_API.Domain.Entities;

namespace ContactHub_API.Infrastructure.Repositories;

public interface _Interface_APIContatosRepository
{
    public Pessoa GetPessoa(int idPessoa);
    public List<Pessoa> GetPessoas();
    public string CreatePessoa(Pessoa pessoa, out int idPessoaCriada);
    public string UpdatePessoa(Pessoa pessoa);
    public string DeletePessoa(int idPessoa);
    public List<LinkPessoa> GetLinksPessoa(int idPessoa);
    public string CreateLinkPessoa(CreateLinkPessoaRequestDTO linkPessoaResquestDTO);
    public string DeleteLinkPessoa(LinkPessoa linkPessoa);
    public string DeleteLinksPessoa(int idPessoa);
}
