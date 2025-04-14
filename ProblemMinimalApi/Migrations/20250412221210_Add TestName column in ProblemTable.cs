using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTestNamecolumninProblemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestName",
                table: "Problems");
        }
    }
}
