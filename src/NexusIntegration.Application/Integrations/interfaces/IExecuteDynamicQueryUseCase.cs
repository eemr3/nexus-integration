using NexusIntegration.Application.Integrations.DynamicQuery.Dtos;

namespace NexusIntegration.Application.Integrations.interfaces;

public interface IExecuteDynamicQueryUseCase
{
    Task<ExecuteDynamicQueryResult> ExecuteAsync(QueryBodyDto body, CancellationToken ct = default);
}
