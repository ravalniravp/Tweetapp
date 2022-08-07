using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.tweetapp.DAL.lib.Migrations
{
    public partial class AddedTweetReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Tweets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Tweets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TweetReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyMessage = table.Column<string>(type: "nvarchar(144)", maxLength: 144, nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ReplyById = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetReply_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetReply_Users_ReplyById",
                        column: x => x.ReplyById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_UpdatedById",
                table: "Tweets",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TweetReply_ReplyById",
                table: "TweetReply",
                column: "ReplyById");

            migrationBuilder.CreateIndex(
                name: "IX_TweetReply_TweetId",
                table: "TweetReply",
                column: "TweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_UpdatedById",
                table: "Tweets",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_UpdatedById",
                table: "Tweets");

            migrationBuilder.DropTable(
                name: "TweetReply");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_UpdatedById",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Tweets");
        }
    }
}
