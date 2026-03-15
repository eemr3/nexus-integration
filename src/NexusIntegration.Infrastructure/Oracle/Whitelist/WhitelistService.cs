using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using NexusIntegration.Domain.Integrations.Ports;

namespace NexusIntegration.Infrastructure.Oracle.Whitelist;

public class WhitelistService : IWhitelistService
{
    private readonly Dictionary<string, List<string>> _tables;

    public WhitelistService()
    {
        _tables = LoadWhiteList();
    }

    public IReadOnlyList<string> GetAllowedColumns(string table) =>
        _tables.TryGetValue(table.ToUpperInvariant(), out var cols)
        ? cols
        : [];

    public bool IsTableAllowed(string table) =>
       _tables.ContainsKey(table.ToUpperInvariant());


    public IReadOnlyList<string> ListTable() =>
        [.. _tables.Keys];

    public bool IsWritable(string table) =>
        IsTableAllowed(table);


    private static Dictionary<string, List<string>> LoadWhiteList()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames()
            .First(n => n.EndsWith("integration-whitelist.json"));

        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        var root = JsonSerializer.Deserialize<WhitelistRoot>(stream)!;

        return root.Tables.ToDictionary(
            kv => kv.Key.ToUpperInvariant(),
            kv => kv.Value.Select(c => c.ToUpperInvariant()).ToList());
    }

    private record WhitelistRoot(
        [property: JsonPropertyName("tables")]
        Dictionary<string, List<string>> Tables
    );
}
