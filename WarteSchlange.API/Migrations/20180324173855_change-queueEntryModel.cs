using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WarteSchlange.API.Migrations
{
    public partial class changequeueEntryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QueueEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_QueueEntries_Id",
                table: "QueueEntries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_QueueEntries_Id",
                table: "QueueEntries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QueueEntries");
        }
    }
}
