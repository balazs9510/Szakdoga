using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutPlaner.BLL.DTOs;
using WorkoutPlaner.BLL.Validations;
using WorkoutPlaner.Common;
using WorkoutPlaner.Common.Enumerations;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using WorkoutPlaner.Views.Popups;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{
    public class CreateWorkoutPageViewModel : BaseViewModel
    {
        private bool isEditMode = false;
        private WorkoutDTO _newWorkout;
        public WorkoutDTO NewWorkout
        {
            get { return _newWorkout; }
            set
            {
                SetProperty(ref _newWorkout, value);
                if (!string.IsNullOrEmpty(NewWorkout.ValidName.Value))
                {
                    NewWorkout.Name = value.ValidName.Value;
                }
            }
        }
        private bool _isValidModel;
        public bool IsValidModel
        {
            get { return _isValidModel; }
            set { SetProperty(ref _isValidModel, value); }
        }

        private List<Exercise> AllExercise;
        private ObservableCollection<Exercise> _exercises;
        public ObservableCollection<Exercise> DisplayExercises
        {
            get { return _exercises; }
            set { SetProperty(ref _exercises, value); }
        }
        private ObservableCollection<ExerciseItemDTO> _addedExercises;
        public ObservableCollection<ExerciseItemDTO> AddedExercises
        {
            get { return _addedExercises; }
            set
            {
                SetProperty(ref _addedExercises, value);
                IsValidModel = ValidateModel();
            }
        }

        private Exercise _selectedExercise;
        public Exercise SelectedExercise
        {
            get { return _selectedExercise; }
            set
            {

                SetProperty(ref _selectedExercise, value);
                if (_selectedExercise != null)
                {
                    MessagingCenter.Send(this, MessagingKeys.EditExerciseItem, _selectedExercise);
                }
            }
        }
        public ICommand AddExerciseCommand => new Command(() => AddExercise());
        public ICommand ValidateWorkoutNameCommand => new Command(() => { NewWorkout.ValidName.Validate(); IsValidModel = ValidateModel(); });
        public ICommand SearchCommand => new Command<string>((filter) => FilterExercises(filter));
        public ICommand EditExerciseItemCommand => new Command<object>((filter) =>
        {
            NewMethod(filter);
        });
        public ICommand SaveCommand => new Command(() => SaveAsync());

        public CreateWorkoutPageViewModel(INavigationService navigationService, IDatabaseService dbService) :
            base(navigationService, dbService)
        {
            IsValidModel = false;
            NewWorkout = new WorkoutDTO()
            {
                User = CurrentUser,

            };
            NewWorkout.ValidName.Value = string.Empty;
            NewWorkout.ValidName.Validations.Add(new MinimumSixCharRule<string>()
            { ValidationMessage = ValidationMessages.StringMustBeSixLengthLong });
            AllExercise = _context.Exercises.Include(e => e.MuscleGroup).ToList();
            DisplayExercises = new ObservableCollection<Exercise>(AllExercise);
            AddedExercises = new ObservableCollection<ExerciseItemDTO>();
        }

        private void SubscribeForEvents()
        {
            MessagingCenter.Subscribe<ExerciseSerialRepsPopup, ExerciseItem>(this, MessagingKeys.ExerciseRepsSerialAdded, (sender, args) =>
            {
                var exerciseToAdd = new ExerciseItemDTO(args)
                {
                    IsEditable = true,
                };
                NewWorkout.ExerciseItems.Add(exerciseToAdd);
                AddedExercises = new ObservableCollection<ExerciseItemDTO>(NewWorkout.ExerciseItems);
            });
            MessagingCenter.Subscribe<ExerciseSerialRepsPopup, ExerciseItemDTO>(this, MessagingKeys.ExerciseItemEdited, (sender, args) =>
            {
                NewWorkout.ExerciseItems[NewWorkout.ExerciseItems.FindIndex(e => e.Id == args.Id)] = args;
                AddedExercises = new ObservableCollection<ExerciseItemDTO>(NewWorkout.ExerciseItems);
            });
            MessagingCenter.Subscribe<ExerciseItem, ExerciseItem>(this, MessagingKeys.DeleteExerciseItem, (sender, args) =>
            {
                var toDeleteItem = NewWorkout.ExerciseItems.SingleOrDefault(e => e.Id == args.Id);
                NewWorkout.ExerciseItems.Remove(toDeleteItem);
                AddedExercises = new ObservableCollection<ExerciseItemDTO>(NewWorkout.ExerciseItems);
                IsValidModel = ValidateModel();
            });
        }
        private void UnsubscribeForEvents()
        {
            MessagingCenter.Unsubscribe<ExerciseSerialRepsPopup, int[]>(this, MessagingKeys.ExerciseRepsSerialAdded);
            MessagingCenter.Unsubscribe<ExerciseSerialRepsPopup, ExerciseItem>(this, MessagingKeys.ExerciseItemEdited);
            MessagingCenter.Unsubscribe<ExerciseItem, ExerciseItem>(this, MessagingKeys.DeleteExerciseItem);
        }
        private void FilterExercises(string filter)
        {
            string str;
            if (filter == null)
                str = string.Empty;
            else
                str = filter;
            DisplayExercises = new ObservableCollection<Exercise>(AllExercise.Where(x => x.Name.Contains(str)));
        }
        private bool ValidateModel()
        {
            return NewWorkout.ValidName.Validate() && NewWorkout.ExerciseItems.Count > 0;
        }
        private void AddExercise()
        {
            NavigateAsync(nameof(ExerciseSerialRepsPopup));
        }
        private async void SaveAsync()
        {
            IsBusy = true;
            NetworkService ns = new NetworkService();
            NewWorkout.Name = NewWorkout.ValidName.Value;
            if (!isEditMode)
            {
                var cWorkout = NewWorkout.ToEntity(new Workout());
                var appMode = (int?)GetValueFromProperties(StoreKeys.AppMode);

                //if there is internet connection and user is logged in, then we post the new workout
                if (appMode == 0 && DoIHaveInternet())
                {
                    await PostWorkoutToServer(ns, cWorkout);
                }

                _context.Workouts.Add(cWorkout);
                await _context.SaveChangesAsync();
            }
            else
            {
                var cWorkout = NewWorkout.ToEntity(_context.Workouts.SingleOrDefault(w => w.Id.Equals(NewWorkout.Id)));

                var appMode = (int?)GetValueFromProperties(StoreKeys.AppMode);
                if (appMode == 0 && DoIHaveInternet())
                {
                    if(cWorkout.ServerId == null)
                    {
                        await PostWorkoutToServer(ns, cWorkout);
                        _context.Workouts.Update(cWorkout);
                    }
                    else
                    {
                        await PutWorkoutToServer(ns, cWorkout);
                        _context.Workouts.Update(cWorkout);
                    }
                }
            }
            await _context.SaveChangesAsync();
            NavigateAsync(nameof(MainPage));
            IsBusy = false;
        }

        private async Task PutWorkoutToServer(NetworkService ns, Workout cWorkout)
        {
            foreach (var item in cWorkout.ExerciseItems)
            {
                if (item.State != StoreItemState.DeletedFromClientSide)
                    item.State = StoreItemState.UpdatedFromClientSide;
            }
            var response = await ns.PutWorkout(CurrentUser.Id, cWorkout);
            if (response.IsSuccessStatusCode)
            {
                Workout result = await GetObjectFromResponseAsync<Workout>(response);
                cWorkout.ExerciseItems.RemoveAll(e => true);
                foreach (var item in result.ExerciseItems)
                {
                    var newExerciseItem = new ExerciseItem
                    {
                        Exercise = _context.Exercises.SingleOrDefault(e => e.Name.Equals(item.Exercise.Name)),
                        Reps = item.Reps,
                        State = StoreItemState.EqualsWithServer,
                        ServerId = item.Id,
                        Serial = item.Serial
                    };
                    cWorkout.ExerciseItems.Add(newExerciseItem);
                }
                cWorkout.State = StoreItemState.EqualsWithServer;
            }
        }

        private async Task PostWorkoutToServer(NetworkService ns, Workout cWorkout)
        {
            var response = await ns.PostWorkout(CurrentUser.Id, cWorkout);
            if (response.IsSuccessStatusCode)
            {
                Workout result = await GetObjectFromResponseAsync<Workout>(response);
                cWorkout.ServerId = result.Id;
                cWorkout.ExerciseItems = new List<ExerciseItem>();
                cWorkout.State = StoreItemState.EqualsWithServer;
                foreach (var item in result.ExerciseItems)
                {
                    var newExerciseItem = new ExerciseItem
                    {
                        Exercise = _context.Exercises.SingleOrDefault(e => e.Name.Equals(item.Exercise.Name)),
                        Reps = item.Reps,
                        State = StoreItemState.EqualsWithServer,
                        ServerId = item.Id,
                        Serial = item.Serial
                    };
                    cWorkout.ExerciseItems.Add(newExerciseItem);
                }
            }
        }

        private static void NewMethod(object filter)
        {
            filter.ToString();
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SubscribeForEvents();
            if (parameters[NavigationParamterKeys.WorkoutId] is string workoutId)
            {
                isEditMode = true;
                NewWorkout = new WorkoutDTO(_context.Workouts
                    .Include(w => w.ExerciseItems)
                        .ThenInclude(e => e.Exercise)
                    .SingleOrDefault(w => w.Id.Equals(workoutId)));
                foreach (var item in NewWorkout.ExerciseItems)
                {
                    item.IsEditable = true;
                }
                NewWorkout.ValidName.Value = NewWorkout.Name;
                AddedExercises = new ObservableCollection<ExerciseItemDTO>(NewWorkout.ExerciseItems);
            }
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            UnsubscribeForEvents();
            if (isEditMode)
            {
                MessagingCenter.Send(this, MessagingKeys.BackPressed, NewWorkout);
                parameters.Add(NavigationParamterKeys.Workout, NewWorkout);
            }
        }
    }
}
