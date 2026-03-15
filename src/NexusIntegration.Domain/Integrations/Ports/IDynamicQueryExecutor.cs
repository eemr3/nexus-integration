namespace NexusIntegration.Domain.Integrations.Ports;

public interface IDynamicQueryExecutor
{
    Task<IReadOnlyList<IReadOnlyDictionary<string, object?>>> ExecuteAsync(
        string sql,
        IReadOnlyDictionary<string, object?> bilns,
        CancellationToken ct = default
    );
}
