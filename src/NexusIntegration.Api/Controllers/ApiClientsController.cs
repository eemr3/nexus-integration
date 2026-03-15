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
    private readonly IRotateSecretUseCase _rotateSecretUseCase;
    public ApiClientsController(
        ICreateApiClientUseCase createUseCase,
        IDeactivateClientUseCase deactivateUseCase,
        IRotateSecretUseCase rotateSecretUseCase)
    {
        _createUseCase = createUseCase;
        _deactivateUseCase = deactivateUseCase;
        _rotateSecretUseCase = rotateSecretUseCase;
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

    [HttpPut("{id}/rotate-secret")]
    public async Task<IActionResult> RotateSecret(Guid id)
    {
        var result = await _rotateSecretUseCase.ExecuteAsync(id);

        return Ok(result);
    }
}
