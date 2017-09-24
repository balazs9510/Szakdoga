using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;
using WorkoutPlaner.Common;
using Xamarin.Forms;

namespace WorkoutPlaner.DAL.Model
{
    public class ExerciseItem : StoreItem
    {
        public string Id { get; set; }
        public string ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public string WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public virtual List<DoneExerciseItem> DoneExerciseItem { get; set; }
        public int Reps { get; set; }
        public int Serial { get; set; }
        public ExerciseItem(Exercise ex)
        {
            Exercise = ex;
            Reps = 1;
            Serial = 1;
        }
        public ExerciseItem()
        {

        }
    }
}
