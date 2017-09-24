using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WorkoutPlaner.DAL.Model;

namespace WorkoutPlaner.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20170804085350_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

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

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
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
                        .WithMany()
                        .HasForeignKey("ExerciseId");

                    b.HasOne("WorkoutPlaner.DAL.Model.Workout")
                        .WithMany("ExerciseItems")
                        .HasForeignKey("WorkoutId");
                });
        }
    }
}
