using System;

namespace WorkoutPlaner.DAL.Model
{
    public class DoneExerciseItem : StoreItem
    {
        public string Id { get; set; }
        public ExerciseItem ExerciseItem { get; set; }
        public bool IsCompleted { get; set; }
        public TimeSpan CompleteTime { get; set; }
        public string DoneWorkoutId { get; set; }
        public DoneWorkout DoneWorkout { get; set; }
        public DoneExerciseItem()
        {
            ExerciseItem = new ExerciseItem();
            IsCompleted = false;
            CompleteTime = TimeSpan.MaxValue;
        }
    }
}
