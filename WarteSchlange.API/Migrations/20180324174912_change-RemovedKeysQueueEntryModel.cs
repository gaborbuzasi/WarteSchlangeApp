using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WarteSchlange.API.Migrations
{
    public partial class changeRemovedKeysQueueEntryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_QueueEntries_Id",
                table: "QueueEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueEntries",
                table: "QueueEntries");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QueueEntries",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueEntries",
                table: "QueueEntries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QueueEntries",
                table: "QueueEntries");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QueueEntries",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_QueueEntries_Id",
                table: "QueueEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueueEntries",
                table: "QueueEntries",
                columns: new[] { "QueueId", "UserId" });
        }
    }
}
