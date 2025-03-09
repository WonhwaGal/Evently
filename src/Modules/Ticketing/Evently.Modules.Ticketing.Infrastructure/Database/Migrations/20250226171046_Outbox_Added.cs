using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Modules.Ticketing.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Outbox_Added : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "ticketing",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                error = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_messages", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "ticketing");
    }
}
