using Microsoft.EntityFrameworkCore;

using Shared.Entities;

namespace API.Entities
{
public class UCContext : DbContext
{
    private readonly string _connectionString = "";

    public UCContext(IConfiguration config)
    {
        _connectionString = config["ConnectionStringUC"] ?? "";
    }

    public UCContext(DbContextOptions<UCContext> options) : base(options)
    {
    }

    public DbSet<LinkEntity> Links { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_connectionString);
}
}
