using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class addedTokenStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 16, 16, 25, 24, 794, DateTimeKind.Local).AddTicks(9825),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3478));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 16, 16, 25, 24, 794, DateTimeKind.Local).AddTicks(9714),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 16, 16, 25, 24, 794, DateTimeKind.Local).AddTicks(1726),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 635, DateTimeKind.Local).AddTicks(5672));

            migrationBuilder.CreateTable(
                name: "EmailVerificationTokens",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationTokens", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerificationTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3478),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 16, 16, 25, 24, 794, DateTimeKind.Local).AddTicks(9825));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3368),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 16, 16, 25, 24, 794, DateTimeKind.Local).AddTicks(9714));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 635, DateTimeKind.Local).AddTicks(5672),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 16, 16, 25, 24, 794, DateTimeKind.Local).AddTicks(1726));
        }
    }
}
