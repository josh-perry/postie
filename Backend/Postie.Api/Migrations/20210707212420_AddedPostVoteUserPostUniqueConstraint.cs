using Microsoft.EntityFrameworkCore.Migrations;

namespace Postie.Api.Migrations
{
    public partial class AddedPostVoteUserPostUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVote_Posts_PostID",
                table: "PostVote");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVote_Users_UserID",
                table: "PostVote");

            migrationBuilder.DropIndex(
                name: "IX_PostVote_PostID",
                table: "PostVote");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "PostVote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostID",
                table: "PostVote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostVote_PostID_UserID",
                table: "PostVote",
                columns: new[] { "PostID", "UserID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVote_Posts_PostID",
                table: "PostVote",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVote_Users_UserID",
                table: "PostVote",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVote_Posts_PostID",
                table: "PostVote");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVote_Users_UserID",
                table: "PostVote");

            migrationBuilder.DropIndex(
                name: "IX_PostVote_PostID_UserID",
                table: "PostVote");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "PostVote",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "PostID",
                table: "PostVote",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_PostVote_PostID",
                table: "PostVote",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostVote_Posts_PostID",
                table: "PostVote",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVote_Users_UserID",
                table: "PostVote",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
