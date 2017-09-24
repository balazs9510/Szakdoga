using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.BLL.Model;
using Xamarin.Forms;
using WorkoutPlaner.DAL.Model;

namespace WorkoutPlaner.BLL.DTOs
{
    public class ExerciseItemDTO
    {
        public string Id { get; set; }
        public Exercise Exercise { get; set; }
        public int Reps { get; set; }
        public int Serial { get; set; }
        public bool IsEditable { get; set; }
        public ICommand DeleteCommand => new Command(() => { DeleteMe(); });
        private void DeleteMe()
        {
            MessagingCenter.Send(this, MessagingKeys.DeleteExerciseItem, this);
        }
        public ICommand EditCommand => new Command(() => { EditMe(); });
        private void EditMe()
        {
            MessagingCenter.Send(this, MessagingKeys.EditExerciseItem, this);
        }
        public ExerciseItemDTO()
        {
            Id = Guid.NewGuid().ToString();
            IsEditable = true;
        }
        public ExerciseItemDTO(ExerciseItem e)
        {
            Exercise = e.Exercise;
            Reps = e.Reps;
            Serial = e.Serial;
        }
        public ExerciseItem ToEntity(ExerciseItem e)
        {
            if (e == null) e = new ExerciseItem();

            e.Id = Id ?? Guid.NewGuid().ToString();
            e.Exercise = Exercise;
            e.Reps = Reps;
            e.Serial = Serial;
            return e;
        }
    }
}
