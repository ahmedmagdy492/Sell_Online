using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sell_Online.Migrations
{
    public partial class added_category_to_posts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9711),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 709, DateTimeKind.Local).AddTicks(3138));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9607),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 709, DateTimeKind.Local).AddTicks(2986));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(3126),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 708, DateTimeKind.Local).AddTicks(4744));

            migrationBuilder.AddColumn<long>(
                name: "CategoryID",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PostCategoryID",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostCategoryID",
                table: "Posts",
                column: "PostCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryID",
                table: "Posts",
                column: "PostCategoryID",
                principalTable: "PostCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCategories_PostCategoryID",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostCategoryID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostCategoryID",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 709, DateTimeKind.Local).AddTicks(3138),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9711));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 709, DateTimeKind.Local).AddTicks(2986),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(9607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 708, DateTimeKind.Local).AddTicks(4744),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 30, 11, 21, 24, 874, DateTimeKind.Local).AddTicks(3126));
        }
    }
}
