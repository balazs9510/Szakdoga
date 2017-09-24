using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutPlaner.Server.DAL.Models
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
        public MuscleGroup(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Exercises = new List<Exercise>();
        }
    }

}
