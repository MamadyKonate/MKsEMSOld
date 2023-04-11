using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKsEMS.Migrations
{
    /// <inheritdoc />
    public partial class saltLengToCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Leaves",
                newName: "userEmail");

            migrationBuilder.AddColumn<int>(
                name: "End",
                table: "Credentials",
                type: "INTEGER",
                maxLength: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Start",
                table: "Credentials",
                type: "INTEGER",
                maxLength: 2,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "Leaves",
                newName: "UserEmail");
        }
    }
}
