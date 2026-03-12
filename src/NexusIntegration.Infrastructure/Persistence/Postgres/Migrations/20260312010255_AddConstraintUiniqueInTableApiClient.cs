using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusIntegration.Infrastructure.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintUiniqueInTableApiClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_api_clients_ClientId",
                table: "api_clients",
                column: "ClientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_api_clients_ClientId",
                table: "api_clients");
        }
    }
}
