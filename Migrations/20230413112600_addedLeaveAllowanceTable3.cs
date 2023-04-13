using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKsEMS.Migrations
{
    /// <inheritdoc />
    public partial class addedLeaveAllowanceTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "LeaveAllowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Allowance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllowances", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveAllowances");

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
    }
}
