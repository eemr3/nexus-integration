using NexusIntegration.Application.Integrations.DynamicQuery.Dtos;
using NexusIntegration.Application.Integrations.interfaces;
using NexusIntegration.Domain.Integrations.Ports;
using NexusIntegration.Shared.Exceptions;

namespace NexusIntegration.Application.Integrations.DynamicQuery.UseCases;

public class ExecuteGenericQueryUseCase : IExecuteDynamicQueryUseCase
{
    private readonly IWhitelistService _whitelistService;
    private readonly IDynamicQueryEngine _engine;
    private readonly IDynamicQueryExecutor _executor;

    public ExecuteGenericQueryUseCase(
        IWhitelistService whitelistService,
        IDynamicQueryEngine engine,
        IDynamicQueryExecutor executor
    )
    {
        _whitelistService = whitelistService;
        _engine = engine;
        _executor = executor;
    }

    public async Task<ExecuteDynamicQueryResult> ExecuteAsync(
        QueryBodyDto body,
        CancellationToken ct = default
    )
    {
        var table = Normalize(body.Table);

        if (!_whitelistService.IsTableAllowed(table))
            throw new BadRequestException($"Tabela '{table}' não permitida.");

        var allowedColumns = _whitelistService.GetAllowedColumns(table);
        var allowedSet = new HashSet<string>(allowedColumns);

        var columns = body.Columns?.Select(Normalize).ToList();
        if (columns != null)
        {
            var invalid = columns.Where(c => !allowedSet.Contains(c)).ToList();
            if (invalid.Any())
                throw new BadRequestException($"Colunas não permitidas: {string.Join(", ", invalid)}");

        }

        var filters = NormalizeFilters(body.Filters, allowedSet);
        var orderBy = NormalizeOrderBy(body.OrderBy, allowedSet);

        var input = new DynamicQueryInput(
            table, columns, filters, orderBy,
            body.Limit, body.Offset, body.IncludeTotal
        );

        var queryResult = _engine.Build(input);

        var rows = await _executor.ExecuteAsync(queryResult.Sql, queryResult.Binds, ct);

        var total = 0;
        if (body.IncludeTotal && rows.Count > 0)
        {
            var first = rows[0];
            var cnt = first.GetValueOrDefault("TOTAL_CNT") ?? first.GetValueOrDefault("total_cnt");
            total = Convert.ToInt32(cnt ?? 0);
        }

        var data = rows.Select(r =>
        {
            var d = new Dictionary<string, object?>(r);
            d.Remove("TOTAL_CNT");
            d.Remove("total_cnt");
            return (IReadOnlyDictionary<string, object?>)d;
        }).ToList();

        return new ExecuteDynamicQueryResult
        {
            Data = data,
            Meta = new QueryMeta
            {
                Table = table,
                Count = data.Count,
                Limit = body.Limit,
                Offset = body.Offset,
                Total = total
            }
        };
    }

    private static string Normalize(string? s) => (s ?? "").Trim().ToUpperInvariant();

    private static IReadOnlyDictionary<string, object?>? NormalizeFilters(
        Dictionary<string, object?>? filters,
        HashSet<string> allowed
        )
    {
        if (filters == null || filters.Count == 0) return null;
        var invalid = filters.Keys.Select(Normalize).Where(k => !allowed.Contains(k)).ToList();
        if (invalid.Count > 0)
            throw new BadRequestException($"Filtros com colunas não permitidas: {string.Join(", ", invalid)}");
        return filters.ToDictionary(kv => Normalize(kv.Key), kv => kv.Value);
    }

    private static IReadOnlyList<(string, string)>? NormalizeOrderBy(
        List<OrderByItemDto>? orderBy,
        HashSet<string> allowed
    )
    {
        if (orderBy == null || orderBy.Count == 0) return null;
        var list = orderBy
            .Select(o => (Normalize(o.Column), o.Direction?.ToLowerInvariant() == "desc" ? "desc" : "asc"))
            .ToList();
        var invalid = list.Select(t => t.Item1).Where(c => !allowed.Contains(c)).ToList();
        if (invalid.Count > 0)
            throw new BadRequestException($"OrderBy com colunas não permitidas: {string.Join(", ", invalid)}");

        return list;
    }
}
