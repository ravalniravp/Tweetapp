using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.tweetapp.DAL.lib.Migrations
{
    public partial class ModifyTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagPersonId",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagedUserId",
                table: "ReplyTag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagPersonId",
                table: "Tag",
                column: "TagPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyTag_TagedUserId",
                table: "ReplyTag",
                column: "TagedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyTag_Users_TagedUserId",
                table: "ReplyTag",
                column: "TagedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Users_TagPersonId",
                table: "Tag",
                column: "TagPersonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyTag_Users_TagedUserId",
                table: "ReplyTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Users_TagPersonId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_TagPersonId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_ReplyTag_TagedUserId",
                table: "ReplyTag");

            migrationBuilder.DropColumn(
                name: "TagPersonId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagedUserId",
                table: "ReplyTag");
        }
    }
}
