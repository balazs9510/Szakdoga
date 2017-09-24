using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.DAL.Model;

namespace WorkoutPlaner.ViewModels.Grouping
{
    public class GroupedDoneWorkoutViewModel : ObservableCollection<DoneWorkout>
    {
        public string Week { get; set; }
        public string WeekNumber { get; set; }
    }
}
