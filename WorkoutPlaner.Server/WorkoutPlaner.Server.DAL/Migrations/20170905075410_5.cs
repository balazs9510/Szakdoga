using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlaner.Server.DAL.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ExerciseItems");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DoneWorkouts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DoneExerciseItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ExerciseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "DoneWorkouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "DoneExerciseItems",
                nullable: true);
        }
    }
}
