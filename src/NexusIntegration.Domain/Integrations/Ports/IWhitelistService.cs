namespace NexusIntegration.Domain.Integrations.Ports;

public interface IWhitelistService
{
    bool IsTableAllowed(string tabel);
    IReadOnlyList<string> GetAllowedColumns(string table);
    IReadOnlyList<string> ListTable();
    bool IsWritable(string table);
}
