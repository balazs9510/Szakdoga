using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WorkoutPlaner.Server.Data;

namespace WorkoutPlaner.Server.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170816080216_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.DoneExerciseItem", b =>
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

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.DoneWorkout", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<TimeSpan>("CompleteTime");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Rating");

                    b.Property<string>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("DoneWorkouts");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.Exercise", b =>
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

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.ExerciseItem", b =>
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

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.MuscleGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MuscleGroups");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.Workout", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("Difficulty");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("Age");

                    b.Property<bool>("AutSync");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime?>("LastSucessfulSync");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("Weigth");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkoutPlaner.Server.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.DoneExerciseItem", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.DAL.Models.DoneWorkout")
                        .WithMany("DoneExerciseItems")
                        .HasForeignKey("DoneWorkoutId");

                    b.HasOne("WorkoutPlaner.Server.DAL.Models.ExerciseItem", "ExerciseItem")
                        .WithMany("DoneExerciseItem")
                        .HasForeignKey("ExerciseItemId");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.DoneWorkout", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.Models.ApplicationUser")
                        .WithMany("DoneWorkouts")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("WorkoutPlaner.Server.DAL.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.Exercise", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.DAL.Models.MuscleGroup", "MuscleGroup")
                        .WithMany("Exercises")
                        .HasForeignKey("MuscleGroupId");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.ExerciseItem", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.DAL.Models.Exercise", "Exercise")
                        .WithMany("ExerciseItems")
                        .HasForeignKey("ExerciseId");

                    b.HasOne("WorkoutPlaner.Server.DAL.Models.Workout", "Workout")
                        .WithMany("ExerciseItems")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("WorkoutPlaner.Server.DAL.Models.Workout", b =>
                {
                    b.HasOne("WorkoutPlaner.Server.Models.ApplicationUser")
                        .WithMany("Workouts")
                        .HasForeignKey("ApplicationUserId");
                });
        }
    }
}
