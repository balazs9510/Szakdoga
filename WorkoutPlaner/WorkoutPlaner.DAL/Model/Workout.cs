using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutPlaner.DAL.Model
{
    public class Workout : StoreItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<ExerciseItem> ExerciseItems { get; set; }
        public Workout()
        {
            Name = string.Empty;
            Difficulty = 1;
            ExerciseItems = new List<ExerciseItem>();
        }
    }
}
