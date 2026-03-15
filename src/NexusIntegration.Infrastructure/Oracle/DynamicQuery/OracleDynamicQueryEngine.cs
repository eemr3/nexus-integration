using System.Data.Common;
using System.Text.RegularExpressions;
using NexusIntegration.Domain.Integrations.Ports;

namespace NexusIntegration.Infrastructure.Oracle.DynamicQuery;

public partial class OracleDynamicQueryEngine : IDynamicQueryEngine
{
    [GeneratedRegex(@"^\d{4}-\d{2}-\d{2}$")]
    private static partial Regex IsoDateRegex();

    public DynamicQueryQueryResult Build(DynamicQueryInput input)
    {
        var binds = new Dictionary<string, object?>();
        var bindIndex = 0;

        var cols = input.Columns != null && input.Columns.Count > 0
            ? string.Join(", ", input.Columns)
            : "*";

        var whereParts = new List<string>();
        if (input.Filters != null)
        {
            foreach (var (col, val) in input.Filters)
            {
                BuildFilterCondition(col, val, whereParts, binds, ref bindIndex);
            }
        }

        var whereCaluse = whereParts.Count > 0
            ? "WHERE " + string.Join(" AND ", whereParts)
            : string.Empty;

        var orderCaluse = input.OrderBy != null && input.OrderBy.Count > 0
            ? "ORDER BY " + string.Join(", ", input.OrderBy.Select(o => $"{o.Column} {o.Direction.ToUpperInvariant()}"))
            : string.Empty;

        binds["b_offset"] = input.Offset;
        binds["b_limit"] = input.Limit;

        string sql;
        if (input.IncludeTotal)
        {
            sql = $""" 
                SELECT {cols}, COUNT(*) OVER () AS TOTAL_CNT 
                FROM {input.Table} 
                {whereCaluse} 
                {orderCaluse}
                OFFSET :b_offset ROWS FETCH NEXT :b_limit ROWS ONLY
            """;

        }
        else
        {
            sql = $"""
                SELECT {cols}
                FROM {input.Table}
                {whereCaluse}
                {orderCaluse}
                OFFSET :b_offset ROWS FETCH NEXT :b_limit ROWS ONLY
            """;
        }

        return new DynamicQueryQueryResult(sql, binds);
    }

    private void BuildFilterCondition(
        string col,
        object? val,
        List<string> parts,
        Dictionary<string, object?> binds,
        ref int idx
)
    {
        if (val is null)
        {
            parts.Add($"{col} is NULL");
            return;
        }

        // Array → IN
        if (val is System.Text.Json.JsonElement je && je.ValueKind == System.Text.Json.JsonValueKind.Array)
        {
            var items = je.EnumerateArray().ToList();
            var bindNames = new List<string>();
            foreach (var item in items)
            {
                var bindKey = $"w{++idx}";
                bindNames.Add($":{bindKey}");
                binds[bindKey] = GetJsonValue(item);
            }

            parts.Add($"{col} IN ({string.Join(", ", bindNames)})");
            return;
        }

        if (val is System.Text.Json.JsonElement obj && obj.ValueKind == System.Text.Json.JsonValueKind.Object)
        {
            foreach (var prop in obj.EnumerateObject())
            {
                var bindKey = $"w{++idx}";
                var rawVal = GetJsonValue(prop.Value);

                switch (prop.Name.ToLowerInvariant())
                {
                    case "eq": AddBind(col, "=", bindKey, rawVal, parts, binds); break;
                    case "neq": AddBind(col, "<>", bindKey, rawVal, parts, binds); break;
                    case "gt": AddBind(col, ">", bindKey, rawVal, parts, binds); break;
                    case "gte": AddBind(col, ">=", bindKey, rawVal, parts, binds); break;
                    case "lt": AddBind(col, "<", bindKey, rawVal, parts, binds); break;
                    case "lte": AddBind(col, "<=", bindKey, rawVal, parts, binds); break;
                    case "like":
                        binds[bindKey] = rawVal;
                        parts.Add($"{col} LIKE :{bindKey}");
                        break;
                    case "isnull":
                        parts.Add(rawVal is true ? $"{col} IS NULL" : $"{col} IS NOT NULL");
                        idx--; // não usou bind
                        break;
                    case "in":
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var inItems = prop.Value.EnumerateArray().ToList();
                            var inBinds = new List<string>();
                            foreach (var item in inItems)
                            {
                                var inBindKey = $"w{++idx}";  // ← renomeado para inBindKey
                                inBinds.Add($":{inBindKey}");
                                binds[inBindKey] = GetJsonValue(item);
                            }
                            parts.Add($"{col} IN ({string.Join(", ", inBinds)})");
                        }
                        break;
                }

            }
            return;
        }

        var key = $"w{++idx}";
        var directVal = val is System.Text.Json.JsonElement jd ? GetJsonValue(jd) : val;
        AddBind(col, "=", key, directVal, parts, binds);
    }

    private static void AddBind(
       string col, string op, string bindKey,
       object? val, List<string> parts, Dictionary<string, object?> binds
    )
    {
        if (val is string s && IsoDateRegex().IsMatch(s))
        {
            parts.Add($"{col} {op} TO_DATE(:{bindKey}, 'YYYY-MM-DD')");
            binds[bindKey] = s.Substring(0, 10);
        }
        else
        {
            parts.Add($"{col} {op} :{bindKey}");
            binds[bindKey] = val;
        }
    }

    private static object? GetJsonValue(System.Text.Json.JsonElement el) => el.ValueKind switch
    {
        System.Text.Json.JsonValueKind.String => el.GetString(),
        System.Text.Json.JsonValueKind.Number => el.TryGetInt64(out var l) ? l : el.GetDouble(),
        System.Text.Json.JsonValueKind.True => true,
        System.Text.Json.JsonValueKind.False => false,
        System.Text.Json.JsonValueKind.Null => null,
        _ => el.ToString()
    };
}