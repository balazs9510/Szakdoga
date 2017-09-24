using Microsoft.EntityFrameworkCore;

namespace WorkoutPlaner.DAL.Model
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;
        public ApplicationContext() : base()
        {
            _connectionString = "WorkoutPlaner.db";
        }
        public ApplicationContext(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        public DbSet<ExerciseItem> ExerciseItems { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<DoneWorkout> DoneWorkouts { get; set; }
        public DbSet<DoneExerciseItem> DoneExerciseItems { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
