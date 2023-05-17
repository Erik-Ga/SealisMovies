using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealisMovies.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the foreign key constraint from the "UserId" column
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_UserId",
                table: "Message");

            // Apply the migration to update the database schema

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
