using Microsoft.AspNetCore.Mvc;
using NexusIntegration.Application.Platform.Dtos;
using NexusIntegration.Application.Platform.interfaces;

namespace NexusIntegration.Api.Controllers;


[ApiController]
[Route("api/v1/clients")]
public class ApiClientsController : ControllerBase
{
    private readonly ICreateApiClientUseCase _createUseCase;
    public ApiClientsController(ICreateApiClientUseCase createUseCase)
    {
        _createUseCase = createUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApiClientDto dto)
    {
        var result = await _createUseCase.CreateAsync(dto);

        return Created($"/api/v1/clients/{result.Id}", result);
    }
}
