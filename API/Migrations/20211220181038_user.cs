using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Result_odd_result_oddId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Result_odd");

            migrationBuilder.DropIndex(
                name: "IX_Events_result_oddId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "result_oddId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "sport",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "team2",
                table: "Events",
                newName: "Team2");

            migrationBuilder.RenameColumn(
                name: "team1",
                table: "Events",
                newName: "Team1");

            migrationBuilder.AddColumn<float>(
                name: "Away_Odd",
                table: "Events",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Home_Odd",
                table: "Events",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Tie_Odd",
                table: "Events",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "eventTypeId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sportId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BetState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    value = table.Column<float>(type: "REAL", nullable: false),
                    appUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    betStateId = table.Column<int>(type: "INTEGER", nullable: true),
                    _eventId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bet_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bet_BetState_betStateId",
                        column: x => x.betStateId,
                        principalTable: "BetState",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bet_Events__eventId",
                        column: x => x._eventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_eventTypeId",
                table: "Events",
                column: "eventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_sportId",
                table: "Events",
                column: "sportId");

            migrationBuilder.CreateIndex(
                name: "IX_Bet__eventId",
                table: "Bet",
                column: "_eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Bet_appUserId",
                table: "Bet",
                column: "appUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bet_betStateId",
                table: "Bet",
                column: "betStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventType_eventTypeId",
                table: "Events",
                column: "eventTypeId",
                principalTable: "EventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Sports_sportId",
                table: "Events",
                column: "sportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventType_eventTypeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Sports_sportId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Bet");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropTable(
                name: "BetState");

            migrationBuilder.DropIndex(
                name: "IX_Events_eventTypeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_sportId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Away_Odd",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Home_Odd",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Tie_Odd",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "eventTypeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "sportId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Team2",
                table: "Events",
                newName: "team2");

            migrationBuilder.RenameColumn(
                name: "Team1",
                table: "Events",
                newName: "team1");

            migrationBuilder.AddColumn<int>(
                name: "result_oddId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sport",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Result_odd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    away = table.Column<float>(type: "REAL", nullable: false),
                    home = table.Column<float>(type: "REAL", nullable: false),
                    tie = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result_odd", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_result_oddId",
                table: "Events",
                column: "result_oddId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Result_odd_result_oddId",
                table: "Events",
                column: "result_oddId",
                principalTable: "Result_odd",
                principalColumn: "Id");
        }
    }
}
