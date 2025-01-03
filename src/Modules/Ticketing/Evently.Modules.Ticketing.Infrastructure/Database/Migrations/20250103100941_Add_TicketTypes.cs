using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Modules.Ticketing.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_TicketTypes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ticket_types",
            schema: "ticketing",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                quantity = table.Column<decimal>(type: "decimal(6,0)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_ticket_types", x => x.id);
                table.ForeignKey(
                    name: "fk_ticket_types_events_event_id",
                    column: x => x.event_id,
                    principalSchema: "ticketing",
                    principalTable: "events",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_ticket_types_event_id",
            schema: "ticketing",
            table: "ticket_types",
            column: "event_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ticket_types",
            schema: "ticketing");
    }
}
