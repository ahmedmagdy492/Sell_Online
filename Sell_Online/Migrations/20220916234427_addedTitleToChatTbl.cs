using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class addedTitleToChatTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 17, 1, 44, 27, 417, DateTimeKind.Local).AddTicks(4239),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 17, 1, 38, 43, 517, DateTimeKind.Local).AddTicks(7264));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 17, 1, 44, 27, 417, DateTimeKind.Local).AddTicks(4075),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 17, 1, 38, 43, 517, DateTimeKind.Local).AddTicks(7150));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 17, 1, 44, 27, 416, DateTimeKind.Local).AddTicks(3578),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 17, 1, 38, 43, 516, DateTimeKind.Local).AddTicks(8679));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Chats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Chats");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 17, 1, 38, 43, 517, DateTimeKind.Local).AddTicks(7264),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 17, 1, 44, 27, 417, DateTimeKind.Local).AddTicks(4239));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 17, 1, 38, 43, 517, DateTimeKind.Local).AddTicks(7150),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 17, 1, 44, 27, 417, DateTimeKind.Local).AddTicks(4075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 17, 1, 38, 43, 516, DateTimeKind.Local).AddTicks(8679),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 17, 1, 44, 27, 416, DateTimeKind.Local).AddTicks(3578));
        }
    }
}
