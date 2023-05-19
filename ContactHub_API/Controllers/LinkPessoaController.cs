using ContactHub_API.Domain.DTOs;
using ContactHub_API.Domain.Entities;
using ContactHub_API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactHub_API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LinkPessoaController : ControllerBase
{
    private readonly _Interface_APIContatosRepository _repository;

    public LinkPessoaController(_Interface_APIContatosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<LinkPessoa>> GetLinksPessoa(int idPessoa)
    {
        try
        {
            return Ok(_repository.GetLinksPessoa(idPessoa));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public ActionResult Create(CreateLinkPessoaRequestDTO LinkPessoa)
    {
        try
        {
            return Ok(_repository.CreateLinkPessoa(LinkPessoa));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete]
    public ActionResult Delete(LinkPessoa linkPessoa)
    {
        try
        {
            return Ok(_repository.DeleteLinkPessoa(linkPessoa));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
