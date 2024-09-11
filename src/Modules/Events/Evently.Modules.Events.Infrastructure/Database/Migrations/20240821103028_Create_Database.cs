using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Modules.Events.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Database : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "events");

        migrationBuilder.CreateTable(
            name: "events",
            schema: "events",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                start_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                end_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_events", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "events",
            schema: "events");
    }
}
