using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class addedPostIDToPostViewsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 3, 13, 29, 20, 307, DateTimeKind.Local).AddTicks(2131),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(3043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 3, 13, 29, 20, 307, DateTimeKind.Local).AddTicks(2021),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(2933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 3, 13, 29, 20, 306, DateTimeKind.Local).AddTicks(5742),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 521, DateTimeKind.Local).AddTicks(6115));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(3043),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 3, 13, 29, 20, 307, DateTimeKind.Local).AddTicks(2131));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 522, DateTimeKind.Local).AddTicks(2933),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 3, 13, 29, 20, 307, DateTimeKind.Local).AddTicks(2021));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 7, 2, 23, 26, 56, 521, DateTimeKind.Local).AddTicks(6115),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 7, 3, 13, 29, 20, 306, DateTimeKind.Local).AddTicks(5742));
        }
    }
}
