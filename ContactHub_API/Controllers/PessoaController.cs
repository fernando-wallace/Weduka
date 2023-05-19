using ContactHub_API.Domain.Entities;
using ContactHub_API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactHub_API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PessoaController : ControllerBase
{
    private readonly _Interface_APIContatosRepository _repository;

    public PessoaController(_Interface_APIContatosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<Pessoa> GetPessoa(int idPessoa)
    {
        try
        {
            return Ok(_repository.GetPessoa(idPessoa));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Pessoa>> GetPessoas()
    {
        try
        {
            return Ok(_repository.GetPessoas());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public ActionResult Create(Pessoa pessoa)
    {
        try
        {
            return Ok(_repository.CreatePessoa(pessoa, out _));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public ActionResult Update(Pessoa pessoa)
    {
        try
        {
            return Ok(_repository.UpdatePessoa(pessoa));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public ActionResult Delete(int idPessoa)
    {
        try
        {
            return Ok(_repository.DeletePessoa(idPessoa));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
