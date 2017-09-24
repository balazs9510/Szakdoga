using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlaner.DAL.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerId",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Workouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ExerciseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerId",
                table: "ExerciseItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ExerciseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "DoneWorkouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerId",
                table: "DoneWorkouts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "DoneWorkouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "DoneExerciseItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerId",
                table: "DoneExerciseItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "DoneExerciseItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ExerciseItems");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "ExerciseItems");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ExerciseItems");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DoneWorkouts");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "DoneWorkouts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "DoneWorkouts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DoneExerciseItems");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "DoneExerciseItems");

            migrationBuilder.DropColumn(
                name: "State",
                table: "DoneExerciseItems");
        }
    }
}
