using Microsoft.EntityFrameworkCore;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{
    public class MuscleGroupPageViewModel : BaseViewModel
    {
        /// <summary>All models; loaded from appropriate data source.</summary>
        public ObservableCollection<MuscleGroup> MuscleGroups { get; set; }

        /// <summary> Model pairs, used for two-column list items.</summary>
        public ObservableCollection<ModelPair> ModelPairs { get; set; }
        /// <summary>Use for display exercise list </summary>
        private bool _isExerciseListView;
        public bool IsExerciseListView
        {
            get { return _isExerciseListView; }
            set { SetProperty(ref _isExerciseListView, value); }
        }
        /// <summary>Selected workout, these exercises will be display</summary>
        private MuscleGroup _selectedMuscleGroup;
        public MuscleGroup SelectedMuscleGroup
        {
            get { return _selectedMuscleGroup; }
            set { SetProperty(ref _selectedMuscleGroup, value); }
        }
        public ICommand EmptyCommand => new Command<object>(ListExercisesAsync);

        private async void ListExercisesAsync(object parameter)
        {
            IsBusy = true;
            await Task.Delay(1000);
            if(parameter is MuscleGroup ms)
            {
                SelectedMuscleGroup = ms;
                MessagingCenter.Send(this, MessagingKeys.ListExercise, 0);
                IsExerciseListView = true;
                IsBusy = false;
            }
        }

        public MuscleGroupPageViewModel(INavigationService navigationService, IDatabaseService dbService) :
            base(navigationService, dbService)
        {
            IsExerciseListView = false;
            MuscleGroups = new ObservableCollection<MuscleGroup>(_context.MuscleGroups
                .Include(m => m.Exercises)
                .ToList()
                .OrderBy(o => o.Name));
            ModelPairs = new ObservableCollection<ModelPair>();

            CreateModelPairs();
        }
        // <summary> Creating model pairs from all available model instances.</summary>
        private void CreateModelPairs()
        {
            for (int i = 0; i < MuscleGroups.Count; i += 2)
            {
                MuscleGroup item1 = MuscleGroups[i];
                MuscleGroup item2 = i + 1 < MuscleGroups.Count ? MuscleGroups[i + 1] : null;

                ModelPairs.Add(new ModelPair(item1, item2));
            }
        }
    }
    public class ModelPair : Tuple<MuscleGroup, MuscleGroup>
    {
        public ModelPair(MuscleGroup left, MuscleGroup right)
            : base(left, right ?? CreateEmptyModel()) { }

        private static MuscleGroup CreateEmptyModel()
        {
            return new MuscleGroup();
        }
    }
}
