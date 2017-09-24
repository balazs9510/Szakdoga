using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlaner.Server.DAL.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoneExerciseItem_DoneWorkout_DoneWorkoutId",
                table: "DoneExerciseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DoneExerciseItem_ExerciseItem_ExerciseItemId",
                table: "DoneExerciseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DoneWorkout_AspNetUsers_ApplicationUserId",
                table: "DoneWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_DoneWorkout_Workout_WorkoutId",
                table: "DoneWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_MuscleGroup_MuscleGroupId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseItem_Exercise_ExerciseId",
                table: "ExerciseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseItem_Workout_WorkoutId",
                table: "ExerciseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_AspNetUsers_ApplicationUserId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MuscleGroup",
                table: "MuscleGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseItem",
                table: "ExerciseItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoneWorkout",
                table: "DoneWorkout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoneExerciseItem",
                table: "DoneExerciseItem");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "Workouts");

            migrationBuilder.RenameTable(
                name: "MuscleGroup",
                newName: "MuscleGroups");

            migrationBuilder.RenameTable(
                name: "ExerciseItem",
                newName: "ExerciseItems");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameTable(
                name: "DoneWorkout",
                newName: "DoneWorkouts");

            migrationBuilder.RenameTable(
                name: "DoneExerciseItem",
                newName: "DoneExerciseItems");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_ApplicationUserId",
                table: "Workouts",
                newName: "IX_Workouts_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseItem_WorkoutId",
                table: "ExerciseItems",
                newName: "IX_ExerciseItems_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseItem_ExerciseId",
                table: "ExerciseItems",
                newName: "IX_ExerciseItems_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_MuscleGroupId",
                table: "Exercises",
                newName: "IX_Exercises_MuscleGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneWorkout_WorkoutId",
                table: "DoneWorkouts",
                newName: "IX_DoneWorkouts_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneWorkout_ApplicationUserId",
                table: "DoneWorkouts",
                newName: "IX_DoneWorkouts_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneExerciseItem_ExerciseItemId",
                table: "DoneExerciseItems",
                newName: "IX_DoneExerciseItems_ExerciseItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneExerciseItem_DoneWorkoutId",
                table: "DoneExerciseItems",
                newName: "IX_DoneExerciseItems_DoneWorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MuscleGroups",
                table: "MuscleGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseItems",
                table: "ExerciseItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoneWorkouts",
                table: "DoneWorkouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoneExerciseItems",
                table: "DoneExerciseItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoneExerciseItems_DoneWorkouts_DoneWorkoutId",
                table: "DoneExerciseItems",
                column: "DoneWorkoutId",
                principalTable: "DoneWorkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoneExerciseItems_ExerciseItems_ExerciseItemId",
                table: "DoneExerciseItems",
                column: "ExerciseItemId",
                principalTable: "ExerciseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoneWorkouts_AspNetUsers_ApplicationUserId",
                table: "DoneWorkouts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoneWorkouts_Workouts_WorkoutId",
                table: "DoneWorkouts",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_MuscleGroups_MuscleGroupId",
                table: "Exercises",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseItems_Exercises_ExerciseId",
                table: "ExerciseItems",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseItems_Workouts_WorkoutId",
                table: "ExerciseItems",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoneExerciseItems_DoneWorkouts_DoneWorkoutId",
                table: "DoneExerciseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoneExerciseItems_ExerciseItems_ExerciseItemId",
                table: "DoneExerciseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DoneWorkouts_AspNetUsers_ApplicationUserId",
                table: "DoneWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_DoneWorkouts_Workouts_WorkoutId",
                table: "DoneWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_MuscleGroups_MuscleGroupId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseItems_Exercises_ExerciseId",
                table: "ExerciseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseItems_Workouts_WorkoutId",
                table: "ExerciseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MuscleGroups",
                table: "MuscleGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseItems",
                table: "ExerciseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoneWorkouts",
                table: "DoneWorkouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoneExerciseItems",
                table: "DoneExerciseItems");

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "Workout");

            migrationBuilder.RenameTable(
                name: "MuscleGroups",
                newName: "MuscleGroup");

            migrationBuilder.RenameTable(
                name: "ExerciseItems",
                newName: "ExerciseItem");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameTable(
                name: "DoneWorkouts",
                newName: "DoneWorkout");

            migrationBuilder.RenameTable(
                name: "DoneExerciseItems",
                newName: "DoneExerciseItem");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_ApplicationUserId",
                table: "Workout",
                newName: "IX_Workout_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseItems_WorkoutId",
                table: "ExerciseItem",
                newName: "IX_ExerciseItem_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseItems_ExerciseId",
                table: "ExerciseItem",
                newName: "IX_ExerciseItem_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_MuscleGroupId",
                table: "Exercise",
                newName: "IX_Exercise_MuscleGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneWorkouts_WorkoutId",
                table: "DoneWorkout",
                newName: "IX_DoneWorkout_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneWorkouts_ApplicationUserId",
                table: "DoneWorkout",
                newName: "IX_DoneWorkout_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneExerciseItems_ExerciseItemId",
                table: "DoneExerciseItem",
                newName: "IX_DoneExerciseItem_ExerciseItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DoneExerciseItems_DoneWorkoutId",
                table: "DoneExerciseItem",
                newName: "IX_DoneExerciseItem_DoneWorkoutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MuscleGroup",
                table: "MuscleGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseItem",
                table: "ExerciseItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoneWorkout",
                table: "DoneWorkout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoneExerciseItem",
                table: "DoneExerciseItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoneExerciseItem_DoneWorkout_DoneWorkoutId",
                table: "DoneExerciseItem",
                column: "DoneWorkoutId",
                principalTable: "DoneWorkout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoneExerciseItem_ExerciseItem_ExerciseItemId",
                table: "DoneExerciseItem",
                column: "ExerciseItemId",
                principalTable: "ExerciseItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoneWorkout_AspNetUsers_ApplicationUserId",
                table: "DoneWorkout",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoneWorkout_Workout_WorkoutId",
                table: "DoneWorkout",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_MuscleGroup_MuscleGroupId",
                table: "Exercise",
                column: "MuscleGroupId",
                principalTable: "MuscleGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseItem_Exercise_ExerciseId",
                table: "ExerciseItem",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseItem_Workout_WorkoutId",
                table: "ExerciseItem",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_AspNetUsers_ApplicationUserId",
                table: "Workout",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
