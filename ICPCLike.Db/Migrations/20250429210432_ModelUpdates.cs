using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICPCLike.Db.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "level",
                table: "stages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "role",
                table: "persons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "persons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "level",
                table: "stages");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "persons");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "persons",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
