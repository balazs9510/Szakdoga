using Prism.Navigation;
using System.Linq;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using Xamarin.Forms;
using System;
using Microsoft.EntityFrameworkCore;
using WorkoutPlaner.Resources;
using WorkoutPlaner.BLL.DTOs;
using WorkoutPlaner.Common.Keys;

namespace WorkoutPlaner.ViewModels
{
    public class WorkoutDetailPageViewModel : BaseViewModel
    {
        private WorkoutDTO _workout;
        public WorkoutDTO Workout
        {
            get { return _workout; }
            set { SetProperty(ref _workout, value); }
        }
        public ICommand EditCommand => new Command(() => OpenEditPage());
        public ICommand DeleteCommand => new Command(() => { SendDelete(); });
        public ICommand StartCommand => new Command(() => OpenCompletePage());
        private void SendDelete()
        {
            MessagingCenter.Send(this, MessagingKeys.DeleteWorkoutPopUp);
        }
        public WorkoutDetailPageViewModel(INavigationService navigationService, IDatabaseService dbService) :
            base(navigationService, dbService)
        {
            Workout = new WorkoutDTO();
        }

        private void SubscribeEvents()
        {
            MessagingCenter.Subscribe<ExerciseItem, ExerciseItem>(this, MessagingKeys.EditExerciseItem, (s, a) =>
            {
                Workout.ExerciseItems[Workout.ExerciseItems.FindIndex(e => e.Id == a.Id)] = new ExerciseItemDTO(a);
                var cWorkout = _context.Workouts.SingleOrDefault(w => w.Id.Equals(Workout.Id));
                _context.Workouts.Update(cWorkout);
                RaisePropertyChanged(nameof(Workout.ExerciseItems));
            });
            MessagingCenter.Subscribe<ExerciseItem, ExerciseItem>(this, MessagingKeys.DeleteExerciseItem, (s, a) =>
            {
                var toDeleteItem = Workout.ExerciseItems.SingleOrDefault(e => e.Id == a.Id);
                Workout.ExerciseItems.Remove(toDeleteItem);
                RaisePropertyChanged(nameof(Workout.ExerciseItems));
                var cWorkout = _context.Workouts.SingleOrDefault(w => w.Id.Equals(Workout.Id));
                _context.Workouts.Update(cWorkout);
            });
            MessagingCenter.Subscribe<CreateWorkoutPageViewModel, Workout>(this, MessagingKeys.BackPressed, (s, a) =>
            {
                Workout = new WorkoutDTO(a);
            });
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters[NavigationParamterKeys.WorkoutId] is string workoutId)
                Workout = new WorkoutDTO(_context.Workouts
                    .Include(w => w.ExerciseItems)
                        .ThenInclude(e => e.Exercise)
                    .SingleOrDefault(w => w.Id.Equals(workoutId)));
            SubscribeEvents();
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            MessagingCenter.Unsubscribe<ExerciseItem, ExerciseItem>(this, MessagingKeys.EditExerciseItem);
            MessagingCenter.Unsubscribe<ExerciseItem, ExerciseItem>(this, MessagingKeys.DeleteWorkoutPopUp);
            //MessagingCenter.Unsubscribe<CreateWorkoutPageViewModel, Workout>(this, MessagingKeys.BackPressed);
        }
        private void OpenCompletePage()
        {
            IsBusy = true;
            var parameter = new NavigationParameters
            {
                { NavigationParamterKeys.WorkoutId, Workout.Id }
            };
            NavigateAsync(nameof(CompleteWorkoutPage), parameter);
        }
        private void OpenEditPage()
        {
            IsBusy = true;
            var parameter = new NavigationParameters
            {
                { NavigationParamterKeys.WorkoutId, Workout.Id }
            };
            NavigateAsync(nameof(CreateWorkoutPage), parameter);
        }
        public async void DeleteWorkoutAsync()
        {
            IsBusy = true;
            var cWorkout = _context.Workouts.SingleOrDefault(w => w.Id.Equals(Workout.Id));
            var appMode = (int)GetValueFromProperties(StoreKeys.AppMode);
            if (appMode == 0 && DoIHaveInternet())
            {
                var nS = new NetworkService
                {
                    AuthCookie = GetValueFromProperties(StoreKeys.AuthCookie) as string,
                };
                try
                {
                    var response = await nS.DeleteWorkout(cWorkout.ServerId);
                    if (response.IsSuccessStatusCode)
                    {
                        var connectedDWorkout = _context.DoneWorkouts.Where(w => w.WorkoutId.Equals(cWorkout.Id));
                        if (connectedDWorkout == null)
                        {
                            _context.Workouts.Remove(cWorkout);
                            _context.SaveChanges();
                            await Page.DisplayAlert("", AppResources.SuccessDelete, AppResources.Ok);
                        }
                        else
                        {
                            await Page.DisplayAlert("", AppResources.NotDeleteableWorkout, AppResources.Ok);
                        }
                    }
                    else
                    {
                        DeleteFromClientSideAsync(cWorkout);
                    }
                }
                catch (Exception e)
                {
                    await Page.DisplayAlert(AppResources.Error, e.Message, AppResources.Ok);
                    return;
                }
            }
            else
            {
                DeleteFromClientSideAsync(cWorkout);
            }

            NavigateAsync(nameof(MainPage));
            IsBusy = false;
        }
        private async void DeleteFromClientSideAsync(Workout cWorkout)
        {
            var connectedDWorkout = _context.DoneWorkouts.Where(w => w.WorkoutId.Equals(cWorkout.Id));
            if(connectedDWorkout == null)
            {
                cWorkout.State = Common.Enumerations.StoreItemState.DeletedFromClientSide;
                _context.Workouts.Update(cWorkout);
                _context.SaveChanges();
                await Page.DisplayAlert("", AppResources.SuccessDelete, AppResources.Ok);
            }
            else
            {
                await Page.DisplayAlert("", AppResources.NotDeleteableWorkout, AppResources.Ok);
            }
            
        }
    }
}
