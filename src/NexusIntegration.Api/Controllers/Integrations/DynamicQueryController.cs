using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusIntegration.Application.Integrations.DynamicQuery.Dtos;
using NexusIntegration.Application.Integrations.interfaces;

namespace NexusIntegration.Api.Controllers.Integrations;

[ApiController]
[Route("api/[controller]")]
public class DynamicQueryController : ControllerBase
{
    private readonly IExecuteDynamicQueryUseCase _exectuteUseCase;

    public DynamicQueryController(IExecuteDynamicQueryUseCase exectuteUseCase)
    {
        _exectuteUseCase = exectuteUseCase;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] QueryBodyDto body)
    {
        var result = await _exectuteUseCase.ExecuteAsync(body);

        return Created($"/api/v1/dynamic-query", result);
    }
}
