using ContactHub_API.Domain.Entities;

namespace ContactHub_API.Domain.DTOs;

public class CreateLinkPessoaRequestDTO
{
    public int IdPessoaSolicitante { get; set; }
    public Pessoa? Pessoa { get; set; }
}
