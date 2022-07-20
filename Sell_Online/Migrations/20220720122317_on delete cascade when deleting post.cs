using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class ondeletecascadewhendeletingpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Posts_PostID",
                table: "PostImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostID",
                table: "PostViews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 20, 14, 23, 17, 793, DateTimeKind.Local).AddTicks(4997),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 20, 14, 23, 17, 793, DateTimeKind.Local).AddTicks(4845),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 20, 14, 23, 17, 792, DateTimeKind.Local).AddTicks(7995),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(2505));

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Posts_PostID",
                table: "PostImages",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostID",
                table: "PostViews",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostImages_Posts_PostID",
                table: "PostImages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Posts_PostID",
                table: "PostViews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9625),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 20, 14, 23, 17, 793, DateTimeKind.Local).AddTicks(4997));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(9510),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 20, 14, 23, 17, 793, DateTimeKind.Local).AddTicks(4845));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 13, 1, 24, 51, 95, DateTimeKind.Local).AddTicks(2505),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 20, 14, 23, 17, 792, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.AddForeignKey(
                name: "FK_PostImages_Posts_PostID",
                table: "PostImages",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Posts_PostID",
                table: "PostViews",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
