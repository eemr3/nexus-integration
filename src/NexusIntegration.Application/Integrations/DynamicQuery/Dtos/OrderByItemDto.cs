
namespace NexusIntegration.Application.Integrations.DynamicQuery.Dtos;

public class OrderByItemDto
{
    public string Column { get; set; } = string.Empty;
    public string Direction { get; set; } = "asc";
}
