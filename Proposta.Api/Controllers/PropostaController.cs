using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using Infraestructure.Domain.Services.Commands;

[ApiController]
[Route("{tenant_database}/api/[controller]")]
public class PropostaController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropostaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePropostaCommand command, [FromRoute] string tenant_database)
    {
        // Só envia o comando -> MediatR fará o Publish na fila
        await _mediator.Send(command);
        return Accepted(new { Status = "Enviado para fila", Tenant = tenant_database });
    }
}
