using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutPlaner.Server.DAL.Models
{
    public class Exercise
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public string MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public virtual List<ExerciseItem> ExerciseItems { get; set; }
        public Exercise()
        {
            Name = string.Empty;
            Description = string.Empty;
            Difficulty = 1;
        }
    }
}
