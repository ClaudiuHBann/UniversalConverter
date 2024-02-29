using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
/// <inheritdoc />
public partial class Ranks : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "rank",
            columns: table =>
                new { id = table.Column<long>(type: "bigint", nullable: false)
                               .Annotation("Npgsql:ValueGenerationStrategy",
                                           NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                      converter = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                      conversions = table.Column<long>(type: "bigint", nullable: false) },
            constraints: table =>
            { table.PrimaryKey("PK_rank", x => x.id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "rank");
    }
}
}
