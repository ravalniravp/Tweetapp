using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.tweetapp.DAL.lib.Migrations
{
    public partial class Correcttweetreplyuserskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetReply_Users_ReplyById",
                table: "TweetReply");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TweetReply");

            migrationBuilder.RenameColumn(
                name: "ReplyById",
                table: "TweetReply",
                newName: "RepliedById");

            migrationBuilder.RenameIndex(
                name: "IX_TweetReply_ReplyById",
                table: "TweetReply",
                newName: "IX_TweetReply_RepliedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TweetReply_Users_RepliedById",
                table: "TweetReply",
                column: "RepliedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetReply_Users_RepliedById",
                table: "TweetReply");

            migrationBuilder.RenameColumn(
                name: "RepliedById",
                table: "TweetReply",
                newName: "ReplyById");

            migrationBuilder.RenameIndex(
                name: "IX_TweetReply_RepliedById",
                table: "TweetReply",
                newName: "IX_TweetReply_ReplyById");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TweetReply",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TweetReply_Users_ReplyById",
                table: "TweetReply",
                column: "ReplyById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
