using Microsoft.EntityFrameworkCore;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.Controls;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{
    public class CompleteWorkoutPageViewModel : BaseViewModel
    {
        #region Properties
        private bool _isEnd = false;
        public bool IsEnd
        {
            get { return _isEnd; }
            set
            {
                SetProperty(ref _isEnd, value);
                ((Command)StartCommand).ChangeCanExecute();
            }
        }

        /// <summary>New instance to save to db.</summary>
        private DoneWorkout _doneWorkout;
        public DoneWorkout DoneWorkout
        {
            get { return _doneWorkout; }
            set { SetProperty(ref _doneWorkout, value); }
        }
        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }
        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        public ICommand StartCommand => new Command(
            () =>
            {
                IsRunning = true;
                StartTimer();
            },
            () => !IsEnd);
        public ICommand PauseCommand => new Command(() => { IsRunning = false; });
        public ICommand StopCommand => new Command(() => Stop());
        public ICommand SaveAndGoBackCommand => new Command(() => SaveAsync());

        #endregion
        #region Constructor
        public CompleteWorkoutPageViewModel(INavigationService navigationService, IDatabaseService dbService) :
            base(navigationService, dbService)
        {
            IsRunning = false;
            IsEnd = false;
            DoneWorkout = new DoneWorkout();
            Time = new TimeSpan(0, 0, 0);
        }
        #endregion

        private void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (IsRunning)
                    Time = Time.Add(TimeSpan.FromSeconds(1));
                return IsRunning;
            });
        }
        private async void SaveAsync()
        {
            IsBusy = true;
            _context.DoneWorkouts.Add(DoneWorkout);
            _context.SaveChanges();
            DoneWorkout.CompleteTime = Time;
            DoneWorkout.UserId = CurrentUser.Id;  
            _context.DoneWorkouts.Update(DoneWorkout);
            await _context.SaveChangesAsync();
            try
            {
                var networkService = new NetworkService();
                var response = await networkService.PostDoneWorkout(CurrentUser.Id, DoneWorkout);
                if (response.IsSuccessStatusCode)
                {
                    DoneWorkout sDoneWorkout = await GetObjectFromResponseAsync<DoneWorkout>(response);
                    DoneWorkout.ServerId = sDoneWorkout.ServerId;
                    DoneWorkout.State = Common.Enumerations.StoreItemState.EqualsWithServer;

                    _context.DoneWorkouts.Update(DoneWorkout);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                await Page.DisplayAlert(AppResources.Error, AppResources.ConnectionError, AppResources.Ok);
            }
            NavigateAsync(nameof(MainPage));
            IsBusy = false;
        }
        private void Stop()
        {
            IsRunning = false;
            MessagingCenter.Send(this, MessagingKeys.StopWorkoutPressed);
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters[NavigationParamterKeys.WorkoutId] is string workoutId)
            {

                DoneWorkout = new DoneWorkout()
                {
                    WorkoutId = _context.Workouts.SingleOrDefault(w => w.Id.Equals(workoutId)).Id,
                    DoneExerciseItems = _context.ExerciseItems.Include(e => e.Exercise)
                        .Where(e => e.Workout.Id.Equals(workoutId))
                        .Select(e => new DoneExerciseItem { ExerciseItem = e }).ToList(),
                    Date = DateTime.Now,
                };

            }
            SubsrcibeForEvents();
        }

        private void SubsrcibeForEvents()
        {
            MessagingCenter.Subscribe<CheckBox, object>(this, MessagingKeys.DoneExerciseCheckChanged, (s, a) =>
            {
                if (a is DoneExerciseItem item)
                {
                    item.CompleteTime = Time;
                    if (!DoneWorkout.DoneExerciseItems.Select(x => x.IsCompleted).ToList().Any(c => c == false))
                    {
                        IsRunning = false;
                        IsEnd = true;
                    }
                }
            });
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            MessagingCenter.Unsubscribe<CheckBox, object>(this, MessagingKeys.DoneExerciseCheckChanged);
        }
    }
}
