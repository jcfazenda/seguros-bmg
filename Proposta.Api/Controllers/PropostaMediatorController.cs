using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Infraestructure.Domain.Repository.Interface;
using Infraestructure.Domain.Services.Commands;
using MediatR;

[Produces("application/json")]
[Route("{tenant_database}/api/[controller]")]
public class PropostaMediatorController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropostaMediatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePropostaCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { PropostaId = id });
    }
}
