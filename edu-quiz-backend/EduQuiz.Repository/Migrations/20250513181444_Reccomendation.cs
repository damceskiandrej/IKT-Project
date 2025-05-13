using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduQuiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Reccomendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Reccomendations",
                newName: "Question");

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Reccomendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Reccomendations");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "Reccomendations",
                newName: "Text");
        }
    }
}
