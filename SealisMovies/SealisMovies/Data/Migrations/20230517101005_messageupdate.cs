using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SealisMovies.Data.Migrations
{
    /// <inheritdoc />
    public partial class messageupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Message",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "Reciever",
                table: "Message",
                newName: "SenderId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecieverId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Message",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Message",
                newName: "Reciever");
        }
    }
}
