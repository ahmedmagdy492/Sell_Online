using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class fixed_columns_names_inPost_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(3043),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7239));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(2933),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7047));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 521, DateTimeKind.Local).AddTicks(6115),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 325, DateTimeKind.Local).AddTicks(9015));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7239),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(3043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 326, DateTimeKind.Local).AddTicks(7047),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(2933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 16, 36, 45, 325, DateTimeKind.Local).AddTicks(9015),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 521, DateTimeKind.Local).AddTicks(6115));

            migrationBuilder.AddColumn<long>(
                name: "CategoryID",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "StateID",
                table: "Posts",
                type: "smallint",
                nullable: true);
        }
    }
}
