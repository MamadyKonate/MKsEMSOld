using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKsEMS.Migrations
{
    /// <inheritdoc />
    public partial class ConstraintsAddedToFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Users",
                newName: "ManagerEmail");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "LeaveTypes",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerEmail",
                table: "Users",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LeaveTypes",
                newName: "name");
        }
    }
}
