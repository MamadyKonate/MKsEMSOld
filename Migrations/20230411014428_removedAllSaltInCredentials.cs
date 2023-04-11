using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKsEMS.Migrations
{
    /// <inheritdoc />
    public partial class removedAllSaltInCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "SaltEnd",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "SaltStart",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Credentials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "End",
                table: "Credentials",
                type: "INTEGER",
                maxLength: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SaltEnd",
                table: "Credentials",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltStart",
                table: "Credentials",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Start",
                table: "Credentials",
                type: "INTEGER",
                maxLength: 2,
                nullable: false,
                defaultValue: 0);
        }
    }
}
