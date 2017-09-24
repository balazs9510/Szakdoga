using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlaner.DAL.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DoneWorkouts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    AutSync = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    LastSucessfulSync = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Weigth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneWorkouts_UserId",
                table: "DoneWorkouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoneWorkouts_Users_UserId",
                table: "DoneWorkouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoneWorkouts_Users_UserId",
                table: "DoneWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_DoneWorkouts_UserId",
                table: "DoneWorkouts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DoneWorkouts");
        }
    }
}
