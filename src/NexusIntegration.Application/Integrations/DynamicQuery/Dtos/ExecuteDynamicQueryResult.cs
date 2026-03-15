namespace NexusIntegration.Application.Integrations.DynamicQuery.Dtos;

public class ExecuteDynamicQueryResult
{
    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Data { get; set; } = [];
    public QueryMeta Meta { get; set; } = new();
}

public class QueryMeta
{
    public string Table { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Total { get; set; }
}
