using System.Collections.ObjectModel;

namespace NexusIntegration.Application.Integrations.DynamicQuery.Dtos;

public class QueryBodyDto
{
    public string Table { get; set; } = string.Empty;
    public List<string>? Columns { get; set; }
    public Dictionary<string, object?>? Filters { get; set; }
    public List<OrderByItemDto>? OrderBy { get; set; }
    public int Limit { get; set; } = 50;
    public int Offset { get; set; } = 0;
    public bool IncludeTotal { get; set; } = false;

}
