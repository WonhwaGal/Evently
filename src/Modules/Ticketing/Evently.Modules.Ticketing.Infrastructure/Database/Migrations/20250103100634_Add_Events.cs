using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Modules.Ticketing.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Events : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "events",
            schema: "ticketing",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                location = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                starts_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                ends_at_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
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
            schema: "ticketing");
    }
}
