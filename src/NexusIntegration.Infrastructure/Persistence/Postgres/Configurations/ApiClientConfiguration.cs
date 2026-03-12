using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusIntegration.Infrastructure.Persistence.Postgres.OrmEntities;

namespace NexusIntegration.Infrastructure.Persistence.Postgres.Configurations;

public class ApiClientConfiguration : IEntityTypeConfiguration<ApiClientOrmEntity>
{
    public void Configure(EntityTypeBuilder<ApiClientOrmEntity> builder)
    {
        builder.ToTable("api_clients");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ClientId).IsRequired();
        builder.HasIndex(x => x.ClientId).IsUnique();
        builder.Property(x => x.ClientSecretHash).IsRequired();
        builder.Property(x => x.AllowedScopes).HasColumnType("text[]");
        builder.Property(x => x.AllowedOrigins).HasColumnType("text[]");
        builder.Property(x => x.LastUsedAt).IsRequired(false);
        builder.Property(x => x.RateLimitPerMinute).IsRequired();
        builder.Property(x => x.Active).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();


    }
}
