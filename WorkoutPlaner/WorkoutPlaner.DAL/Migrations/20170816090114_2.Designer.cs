using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WorkoutPlaner.DAL.Model;

namespace WorkoutPlaner.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20170816090114_2")]
    partial class _2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<bool>("AutSync");

                    b.Property<string>("Email");

                    b.Property<DateTime>("LastLogin");

                    b.Property<DateTime?>("LastSucessfulSync");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("Weigth");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.DoneExerciseItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("CompleteTime");

                    b.Property<string>("DoneWorkoutId");

                    b.Property<string>("ExerciseItemId");

                    b.Property<bool>("IsCompleted");

                    b.HasKey("Id");

                    b.HasIndex("DoneWorkoutId");

                    b.HasIndex("ExerciseItemId");

                    b.ToTable("DoneExerciseItems");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.DoneWorkout", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("CompleteTime");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Rating");

                    b.Property<string>("UserId");

                    b.Property<string>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("DoneWorkouts");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.Exercise", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("Difficulty");

                    b.Property<string>("MuscleGroupId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MuscleGroupId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.ExerciseItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExerciseId");

                    b.Property<int>("Reps");

                    b.Property<int>("Serial");

                    b.Property<string>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("ExerciseItems");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.MuscleGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MuscleGroups");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.Workout", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Difficulty");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDeletedFromServer");

                    b.Property<bool>("IsEditedOnServer");

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.DoneExerciseItem", b =>
                {
                    b.HasOne("WorkoutPlaner.DAL.Model.DoneWorkout")
                        .WithMany("DoneExerciseItems")
                        .HasForeignKey("DoneWorkoutId");

                    b.HasOne("WorkoutPlaner.DAL.Model.ExerciseItem", "ExerciseItem")
                        .WithMany("DoneExerciseItem")
                        .HasForeignKey("ExerciseItemId");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.DoneWorkout", b =>
                {
                    b.HasOne("WorkoutPlaner.DAL.Model.ApplicationUser", "User")
                        .WithMany("DoneWorkouts")
                        .HasForeignKey("UserId");

                    b.HasOne("WorkoutPlaner.DAL.Model.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.Exercise", b =>
                {
                    b.HasOne("WorkoutPlaner.DAL.Model.MuscleGroup", "MuscleGroup")
                        .WithMany("Exercises")
                        .HasForeignKey("MuscleGroupId");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.ExerciseItem", b =>
                {
                    b.HasOne("WorkoutPlaner.DAL.Model.Exercise", "Exercise")
                        .WithMany("ExerciseItems")
                        .HasForeignKey("ExerciseId");

                    b.HasOne("WorkoutPlaner.DAL.Model.Workout", "Workout")
                        .WithMany("ExerciseItems")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("WorkoutPlaner.DAL.Model.Workout", b =>
                {
                    b.HasOne("WorkoutPlaner.DAL.Model.ApplicationUser", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserId");
                });
        }
    }
}
