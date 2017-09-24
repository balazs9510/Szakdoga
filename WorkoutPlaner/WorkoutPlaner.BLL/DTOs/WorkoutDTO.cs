using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkoutPlaner.BLL.Validations;
using WorkoutPlaner.DAL.Model;

namespace WorkoutPlaner.BLL.DTOs
{
    public class WorkoutDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ValidatableObject<string> ValidName { get; set; }
        public int Difficulty { get; set; }
        public ApplicationUser User { get; set; }
        public List<ExerciseItemDTO> ExerciseItems { get; set; }
        public WorkoutDTO()
        {
            ValidName = new ValidatableObject<string>();
            Name = string.Empty;
            Id = Guid.NewGuid().ToString();
            ExerciseItems = new List<ExerciseItemDTO>();
        }
        public WorkoutDTO(Workout w)
        {
            Id = w.Id;
            Name = w.Name;
            ValidName = new ValidatableObject<string>();
            ValidName.Value = Name;
            Difficulty = w.Difficulty;
            User = w.User;
            ExerciseItems = w.ExerciseItems.Select(e => new ExerciseItemDTO(e)).ToList();
        }
        public Workout ToEntity(Workout w)
        {
            if (w == null) w = new Workout();

            w.Id = Id ?? Guid.NewGuid().ToString();
            w.Difficulty = Difficulty;
            w.ExerciseItems = ExerciseItems
                .Select(e => e.ToEntity(w.ExerciseItems.SingleOrDefault(ei => ei.Id == e.Id))).ToList();
            w.User = User;
            w.Name = Name;
            return w;
        }
    }
}
