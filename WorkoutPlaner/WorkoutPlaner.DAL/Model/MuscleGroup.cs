using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

namespace WorkoutPlaner.DAL.Model
{
    public class MuscleGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; }
        public MuscleGroup()
        {
            Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            Exercises = new List<Exercise>();
        }
    }
}
