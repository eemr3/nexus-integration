using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NexusIntegration.Application.Auth.Dtos;
using NexusIntegration.Application.Auth.Interfaces;

namespace NexusIntegration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IGenerateTokenUseCase _generateTokenUseCase;

    public AuthController(IGenerateTokenUseCase generateTokenUseCase)
    {
        _generateTokenUseCase = generateTokenUseCase;
    }

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken([FromBody] TokenRequestDto request)
    {
        var result = await _generateTokenUseCase.ExecuteAsync(request);

        return Ok(result);
    }
}
