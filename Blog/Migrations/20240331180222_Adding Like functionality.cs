using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class AddingLikefunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticlesLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesLikes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleId",
                table: "Articles",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Articles_ArticleId",
                table: "Articles",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Articles_ArticleId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticlesLikes");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Articles");
        }
    }
}
