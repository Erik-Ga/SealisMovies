using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealisMovies.Data.Migrations
{
    /// <inheritdoc />
    public partial class reportbutton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reported",
                table: "Discussions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reported",
                table: "Discussions");
        }
    }
}
