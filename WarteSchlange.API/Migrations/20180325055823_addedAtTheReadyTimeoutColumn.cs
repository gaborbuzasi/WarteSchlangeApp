using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WarteSchlange.API.Migrations
{
    public partial class addedAtTheReadyTimeoutColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtTheReadyTimeout",
                table: "Queues",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasReadyAt",
                table: "QueueEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtTheReadyTimeout",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "WasReadyAt",
                table: "QueueEntries");
        }
    }
}
