using Microsoft.EntityFrameworkCore;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WorkoutPlaner.BLL.DTOs;
using WorkoutPlaner.Common;
using WorkoutPlaner.Common.Enumerations;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{

    public class WorkoutsPageViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<WorkoutDTO> _workouts;
        public ObservableCollection<WorkoutDTO> Workouts
        {
            get { return _workouts; }
            set { SetProperty(ref _workouts, value); }
        }
        private WorkoutDTO _selectedItem;
        public WorkoutDTO SelectedItem
        {
            get { return _selectedItem; }
            set {
                SetProperty(ref _selectedItem, value);
                if (_selectedItem != null)
                {
                    var parameters = new NavigationParameters
                    {
                        { NavigationParamterKeys.WorkoutId, SelectedItem.Id }
                    };
                    _selectedItem = null;
                    NavigateAsync(nameof(WorkoutDetailPage), parameters);
                    
                    IsBusy = false;
                }                    
            }
        }
        public ICommand CreateWorkoutCommand => new Command(() => this.NavigateAsync(nameof(CreateWorkoutPage)));
        #endregion
        public WorkoutsPageViewModel(INavigationService navigationService, IDatabaseService dbService) :
            base(navigationService, dbService)
        {
            
            List<Workout> w = null;
            try
            {
                w = _context.Workouts
                    .Where(ww => ww.User.Id.Equals(CurrentUser.Id) && 
                           ww.State != StoreItemState.DeletedFromClientSide)
                    .ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            if (w != null)
            {
                Workouts = new ObservableCollection<WorkoutDTO>(w.Select(a => new WorkoutDTO(a)));
            }
            else
                Workouts = new ObservableCollection<WorkoutDTO>();
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var loadedItems = _context.Workouts
                .Include(ww => ww.User)
                .Where(ww => ww.User.Id.Equals(CurrentUser.Id) &&
                             ww.State != StoreItemState.DeletedFromClientSide)
                .ToList();
            Workouts = new ObservableCollection<WorkoutDTO>(loadedItems.Select(w => new WorkoutDTO(w)));
        }
    }
}
