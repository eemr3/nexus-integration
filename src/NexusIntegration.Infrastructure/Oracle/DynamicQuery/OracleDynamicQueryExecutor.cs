using Dapper;
using NexusIntegration.Domain.Integrations.Ports;
using Oracle.ManagedDataAccess.Client;

namespace NexusIntegration.Infrastructure.Oracle.DynamicQuery;

public class OracleDynamicQueryExecutor : IDynamicQueryExecutor
{
    private readonly string _connectionString;

    public OracleDynamicQueryExecutor(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IReadOnlyList<IReadOnlyDictionary<string, object?>>> ExecuteAsync(
        string sql,
        IReadOnlyDictionary<string, object?> binds,
        CancellationToken ct = default)
    {
        await using var conn = new OracleConnection(_connectionString);
        await conn.OpenAsync(ct);

        var parameters = new DynamicParameters();
        foreach (var (key, value) in binds)
            parameters.Add(key, value);

        var rows = await conn.QueryAsync(sql, parameters);

        return rows.Select(row =>
        {
            var dict = (IDictionary<string, object>)row;
            return (IReadOnlyDictionary<string, object?>)
                dict.ToDictionary(kv => kv.Key, kv => (object?)kv.Value);
        }).ToList();

    }
}
