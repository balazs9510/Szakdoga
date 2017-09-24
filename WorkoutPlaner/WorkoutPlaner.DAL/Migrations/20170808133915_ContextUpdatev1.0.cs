using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlaner.DAL.Migrations
{
    public partial class ContextUpdatev10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoneWorkouts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CompleteTime = table.Column<TimeSpan>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneWorkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoneWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoneExerciseItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CompleteTime = table.Column<TimeSpan>(nullable: false),
                    DoneWorkoutId = table.Column<string>(nullable: true),
                    ExerciseItemId = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneExerciseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoneExerciseItems_DoneWorkouts_DoneWorkoutId",
                        column: x => x.DoneWorkoutId,
                        principalTable: "DoneWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoneExerciseItems_ExerciseItems_ExerciseItemId",
                        column: x => x.ExerciseItemId,
                        principalTable: "ExerciseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoneExerciseItems_DoneWorkoutId",
                table: "DoneExerciseItems",
                column: "DoneWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneExerciseItems_ExerciseItemId",
                table: "DoneExerciseItems",
                column: "ExerciseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneWorkouts_WorkoutId",
                table: "DoneWorkouts",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoneExerciseItems");

            migrationBuilder.DropTable(
                name: "DoneWorkouts");
        }
    }
}
