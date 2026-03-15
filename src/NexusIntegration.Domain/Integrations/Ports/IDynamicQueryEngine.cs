

namespace NexusIntegration.Domain.Integrations.Ports;

public record DynamicQueryInput(
    string Table,
    IReadOnlyList<string>? Columns,
    IReadOnlyDictionary<string, object?>? Filters,
    IReadOnlyList<(string Column, string Direction)>? OrderBy,
    int Limit,
    int Offset,
    bool IncludeTotal
);

public record DynamicQueryQueryResult(
    string Sql,
     IReadOnlyDictionary<string, object?> Binds
);

public interface IDynamicQueryEngine
{
    DynamicQueryQueryResult Build(DynamicQueryInput input);
}
