using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKsEMS.Migrations
{
    /// <inheritdoc />
    public partial class addedLeaveAllowanceTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leaves",
                table: "Leaves");

            migrationBuilder.RenameTable(
                name: "Leaves",
                newName: "Leave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leave",
                table: "Leave",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leave",
                table: "Leave");

            migrationBuilder.RenameTable(
                name: "Leave",
                newName: "Leaves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leaves",
                table: "Leaves",
                column: "Id");
        }
    }
}
