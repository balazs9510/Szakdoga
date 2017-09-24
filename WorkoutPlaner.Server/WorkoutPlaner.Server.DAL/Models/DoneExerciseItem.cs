using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutPlaner.Server.DAL.Models
{
    public class DoneExerciseItem : StoreItem
    {
        public string Id { get; set; }
        public ExerciseItem ExerciseItem { get; set; }
        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                
            }
        }
        private TimeSpan _completeTime;
        public TimeSpan CompleteTime
        {
            get { return _completeTime; }
            set
            {
                _completeTime = value;
               
            }
        }

        public DoneExerciseItem()
        {
            ExerciseItem = new ExerciseItem();
            IsCompleted = false;
            CompleteTime = TimeSpan.MaxValue;
        }

      
    }
}
