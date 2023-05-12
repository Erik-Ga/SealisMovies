using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SealisMovies.Models;

#nullable disable

namespace SealisMovies.Data.Migrations
{
    /// <inheritdoc />
    public partial class testfixtwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<Comment>>(
                name: "CommentList",
                table: "Discussions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.RenameColumn(
                name: "Imgage",
                table: "Comment",
                newName: "Image");

            migrationBuilder.CreateTable(
                name: "Inbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageList = table.Column<List<Message>>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inbox_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_UserId",
                table: "Inbox",
                column: "UserId");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inbox");
        }
    }
}
