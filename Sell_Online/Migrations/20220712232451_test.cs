using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_ViewerID",
                table: "PostViews");

            migrationBuilder.DropIndex(
                name: "IX_PostViews_ViewerID",
                table: "PostViews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9625),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 19, 17, 352, DateTimeKind.Local).AddTicks(7483));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9510),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 19, 17, 352, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(2505),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 19, 17, 352, DateTimeKind.Local).AddTicks(834));

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_PostID",
                table: "PostViews",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostID",
                table: "PostViews",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostID",
                table: "PostViews");

            migrationBuilder.DropIndex(
                name: "IX_PostViews_PostID",
                table: "PostViews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 19, 17, 352, DateTimeKind.Local).AddTicks(7483),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 19, 17, 352, DateTimeKind.Local).AddTicks(7360),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 19, 17, 352, DateTimeKind.Local).AddTicks(834),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(2505));

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_ViewerID",
                table: "PostViews",
                column: "ViewerID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_ViewerID",
                table: "PostViews",
                column: "ViewerID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
