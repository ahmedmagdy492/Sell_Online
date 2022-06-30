using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class added_image_Type_Column_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7239),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9711));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7047),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 325, DateTimeKind.Local).AddTicks(9015),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(3126));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9711),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7239));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9607),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7047));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(3126),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 325, DateTimeKind.Local).AddTicks(9015));
        }
    }
}
