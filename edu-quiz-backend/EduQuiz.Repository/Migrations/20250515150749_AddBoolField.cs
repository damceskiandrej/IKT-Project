using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduQuiz.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddBoolField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Reccomendations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "Reccomendations");
        }
    }
}
