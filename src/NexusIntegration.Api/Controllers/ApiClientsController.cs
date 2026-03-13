using Microsoft.AspNetCore.Mvc;
using NexusIntegration.Application.Platform.Dtos;
using NexusIntegration.Application.Platform.interfaces;
using NexusIntegration.Application.Platform.Interfaces;

namespace NexusIntegration.Api.Controllers;


[ApiController]
[Route("api/v1/clients")]
public class ApiClientsController : ControllerBase
{
    private readonly ICreateApiClientUseCase _createUseCase;
    private readonly IDeactivateClientUseCase _deactivateUseCase;
    public ApiClientsController(
        ICreateApiClientUseCase createUseCase,
    IDeactivateClientUseCase deactivateUseCase)
    {
        _createUseCase = createUseCase;
        _deactivateUseCase = deactivateUseCase;

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApiClientDto dto)
    {
        var result = await _createUseCase.CreateAsync(dto);

        return Created($"/api/v1/clients/{result.Id}", result);
    }

    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var result = await _deactivateUseCase.ExecuteAsync(id);

        return Ok(result);
    }
}
