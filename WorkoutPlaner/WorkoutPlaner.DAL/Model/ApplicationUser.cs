using System;
using System.Collections.Generic;
using System.ComponentModel;
using WorkoutPlaner.Common;
using Xamarin.Forms;

namespace WorkoutPlaner.DAL.Model
{
    public class ApplicationUser : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weigth { get; set; }
        private bool _autSync;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool AutSync
        {
            get { return _autSync; }
            set
            {
                if(_autSync != value)
                {
                    _autSync = value;
                    MessagingCenter.Send(this, MessagingKeys.UserChanged);
                }
            }
        }

        public DateTime LastLogin { get; set; }

        private DateTime? _lastSucessfulSync;
        public DateTime? LastSucessfulSync
        {
            get { return _lastSucessfulSync; }
            set
            {
                if(_lastSucessfulSync!= value)
                {
                    _lastSucessfulSync = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastSucessfulSync)));
                }
            }
        }

        public List<DoneWorkout> DoneWorkouts { get; set; }
        public List<Workout> Workouts { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }
        public ApplicationUser()
        {
            Name = string.Empty;
            Age = 0;
            Weigth = 0;
            Workouts = new List<Workout>();
            DoneWorkouts = new List<DoneWorkout>();
        }
        private void UpdateUser(ApplicationContext context)
        {
            context.Users.Update(this);
        }
    }
}
