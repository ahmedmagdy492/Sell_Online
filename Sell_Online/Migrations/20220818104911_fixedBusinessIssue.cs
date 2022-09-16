using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sell_Online.Migrations
{
    public partial class fixedBusinessIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatID",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatID",
                table: "Messages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3478),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 4, 8, 0, 340, DateTimeKind.Local).AddTicks(3070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3368),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 4, 8, 0, 340, DateTimeKind.Local).AddTicks(2956));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 635, DateTimeKind.Local).AddTicks(5672),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 4, 8, 0, 339, DateTimeKind.Local).AddTicks(4570));

            migrationBuilder.AddColumn<string>(
                name: "ReceiverID",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderID",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverID",
                table: "Messages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverID",
                table: "Messages",
                column: "ReceiverID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderID",
                table: "Messages",
                column: "SenderID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReceiverID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "Messages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SoldDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 4, 8, 0, 340, DateTimeKind.Local).AddTicks(3070),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3478));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EditDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 4, 8, 0, 340, DateTimeKind.Local).AddTicks(2956),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 636, DateTimeKind.Local).AddTicks(3368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 18, 4, 8, 0, 339, DateTimeKind.Local).AddTicks(4570),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 18, 12, 49, 11, 635, DateTimeKind.Local).AddTicks(5672));

            migrationBuilder.AddColumn<string>(
                name: "ChatID",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatID = table.Column<string>(type: "text", nullable: false),
                    ReceiverID = table.Column<string>(type: "text", nullable: false),
                    SenderID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatID);
                    table.ForeignKey(
                        name: "FK_Chats_Users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatID",
                table: "Messages",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ReceiverID",
                table: "Chats",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SenderID",
                table: "Chats",
                column: "SenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatID",
                table: "Messages",
                column: "ChatID",
                principalTable: "Chats",
                principalColumn: "ChatID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
