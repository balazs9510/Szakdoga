using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.ViewModels.Grouping;

namespace WorkoutPlaner.ViewModels
{
    public class HistoryPageViewModel : BaseViewModel
    {
        public ObservableCollection<GroupedDoneWorkoutViewModel> GroupedDoneWorkouts { get; set; }
        public HistoryPageViewModel(INavigationService navigationService, IDatabaseService dbService)
            : base(navigationService, dbService)
        {
            GroupedDoneWorkouts = new ObservableCollection<GroupedDoneWorkoutViewModel>();
            InitGroup();
        }
        private void InitGroup()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var doneWorkouts = _context.DoneWorkouts.Include(d=> d.Workout).ToList();
            var testList = TestList();
            var weekList =
                doneWorkouts.Select(dw => cal.GetWeekOfYear(dw.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                .Union(testList.Select(dw => cal.GetWeekOfYear(dw.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)));
            foreach (var item in weekList)
            {
                var weekgroup = new GroupedDoneWorkoutViewModel()
                {
                    Week = item.ToString() + ". " + AppResources.Week,
                    WeekNumber = item.ToString()
                };
                foreach (var workout in doneWorkouts
                    .Where(dw => cal.GetWeekOfYear(dw.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == item))
                {
                    weekgroup.Add(workout);
                }
                foreach (var workout in testList
                    .Where(dw => cal.GetWeekOfYear(dw.Date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == item))
                {
                    weekgroup.Add(workout);
                }
                GroupedDoneWorkouts.Add(weekgroup);
            };
            GroupedDoneWorkouts = new ObservableCollection<GroupedDoneWorkoutViewModel> 
                (GroupedDoneWorkouts.OrderByDescending(g => g.WeekNumber).ToList());
        }
        private List<DoneWorkout> TestList()
        {
            var result = new List<DoneWorkout>();
            for (int i = 0; i < 30; i++)
            {
                var rand = new Random();
                result.Add(new DoneWorkout()
                {
                    Workout = new Workout() { Name = $"Tesztedzés {i}" },
                    Date = DateTime.Now.AddDays(rand.Next(-365,365)),
                    CompleteTime = new TimeSpan(rand.Next(0, 365), rand.Next(0, 60), rand.Next(0, 60))
                });
            }
            return result;
        }
    }
}
