using Microsoft.AspNetCore.Mvc; 
using MediatR; 
using Infraestructure.Domain.Repository.Interface;
using Infraestructure.Domain.Views.Input; 


[ApiController]
[Route("{tenant_database}/api/[controller]")]
public class PropostaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPropostaRepository _proposta;

    public PropostaController(IMediator mediator, IPropostaRepository proposta)
    {
        _mediator = mediator;
        _proposta = proposta;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PropostaMadiatorInput command, [FromRoute] string tenant_database)
    {
        await _mediator.Send(command);
        return Accepted(new { Status = "Enviado para fila", Tenant = tenant_database });
    }

    [HttpGet("Status")]
    public async Task<IActionResult> Status([FromBody] PropostaInput input)
    {
        var propostas = await _proposta.GetByStatusAsync(input.Status); 
        return Response(true, "Sucesso", "Proposta enviada com sucesso", propostas, "success");
    }

    protected new IActionResult Response(bool success, string Title, string Message, object? data, string type)
    {
        return Ok(new
        {
            success,
            Title,
            Message,
            data,
            type
        });
    }
       
}

 