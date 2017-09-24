using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlaner.DAL.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoneExerciseItems_Workouts_WorkoutId",
                table: "DoneExerciseItems");

            migrationBuilder.DropIndex(
                name: "IX_DoneExerciseItems_WorkoutId",
                table: "DoneExerciseItems");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "DoneExerciseItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkoutId",
                table: "DoneExerciseItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoneExerciseItems_WorkoutId",
                table: "DoneExerciseItems",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoneExerciseItems_Workouts_WorkoutId",
                table: "DoneExerciseItems",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
