﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sell_Online.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostStates",
                columns: table => new
                {
                    StateID = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostStates", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    ProfileImageURL = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: true, defaultValue: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatID = table.Column<string>(nullable: false),
                    SenderID = table.Column<string>(nullable: true),
                    ReceiverID = table.Column<string>(nullable: true),
                    RecieverUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatID);
                    table.ForeignKey(
                        name: "FK_Chats_Users_RecieverUserID",
                        column: x => x.RecieverUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    NotificationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 708, DateTimeKind.Local).AddTicks(4744)),
                    IsEdited = table.Column<bool>(nullable: true, defaultValue: false),
                    EditDate = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 709, DateTimeKind.Local).AddTicks(2986)),
                    StateID = table.Column<short>(nullable: true),
                    SoldDate = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2022, 6, 30, 1, 55, 44, 709, DateTimeKind.Local).AddTicks(3138)),
                    UserID = table.Column<string>(nullable: true),
                    PostStatesStateID = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_PostStates_PostStatesStateID",
                        column: x => x.PostStatesStateID,
                        principalTable: "PostStates",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    SentDate = table.Column<DateTime>(nullable: true),
                    ChatID = table.Column<string>(nullable: true),
                    Seen = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "Chats",
                        principalColumn: "ChatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostImages",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true),
                    PostID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostImages_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostViews",
                columns: table => new
                {
                    PostViewID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ViewerID = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    PostID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostViews", x => x.PostViewID);
                    table.ForeignKey(
                        name: "FK_PostViews_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostViews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_RecieverUserID",
                table: "Chats",
                column: "RecieverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SenderID",
                table: "Chats",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatID",
                table: "Messages",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_UserID",
                table: "PhoneNumbers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostImages_PostID",
                table: "PostImages",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostStatesStateID",
                table: "Posts",
                column: "PostStatesStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_PostID",
                table: "PostViews",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_UserID",
                table: "PostViews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "PostImages");

            migrationBuilder.DropTable(
                name: "PostViews");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "PostStates");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
