﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutPlaner.Server.DAL.Models
{
    public class DoneWorkout : StoreItem
    {
        public string Id { get; set; }
        public Workout Workout { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CompleteTime { get; set; }
        public int Rating { get; set; }
        public List<DoneExerciseItem> DoneExerciseItems { get; set; }
        public DoneWorkout()
        {
            Workout = new Workout();
            Date = DateTime.Now;
            DoneExerciseItems = new List<DoneExerciseItem>();
        }
    }
}
