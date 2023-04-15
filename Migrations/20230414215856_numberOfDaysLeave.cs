using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKsEMS.Migrations
{
    /// <inheritdoc />
    public partial class numberOfDaysLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numberOfDays",
                table: "Leaves",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numberOfDays",
                table: "Leaves");
        }
    }
}
