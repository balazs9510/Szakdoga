using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using WorkoutPlaner.Common;
using WorkoutPlaner.Converters;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.ViewModels;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views.Popups;
using WorkoutPlaner.Views.Templates;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class MuscleGroupPage : BasePage
    {
        public ListView ExerciseListView { get; set; }
        public MuscleGroupPage() : base()
        {
            InitWorkoutGrid();
            MessagingCenter.Subscribe<MuscleGroupPageViewModel, int>(this, MessagingKeys.ListExercise, (sender, args) =>
             {
                 InitExerciseList();
             });
        }
        protected override void SetPageToVM()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).Page = this;
        }
        private void InitWorkoutGrid()
        {
            var VM = ((MuscleGroupPageViewModel)BindingContext);
            var workouts = VM.MuscleGroups;
            var grid = new Grid()
            {
                Margin = new Thickness(10),
                RowSpacing = 10,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Start,
                ColumnSpacing = 20
            };
            grid.BindingContext = VM;

            grid.SetBinding(Grid.IsVisibleProperty, nameof(VM.IsExerciseListView),
                converter: new InverseBoolConverter());
            var rows = new RowDefinitionCollection();
            for (int i = 0; i < workouts.Count % 2; i++)
            {
                rows.Add(new RowDefinition());
            }
            grid.RowDefinitions = rows;
            grid.ColumnDefinitions = new ColumnDefinitionCollection()
            {
                new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Star)},
                new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Star)}
            };
            var templateList = new List<View>();
            foreach (var item in workouts)
            {
                var newItem = new MuscleGroupTemplate(item);
                var gesture = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    Command = VM.EmptyCommand,
                    CommandParameter = item
                };
                
                newItem.IsEnabled = true;
                newItem.GestureRecognizers.Add(gesture);
                templateList.Add(newItem);
            }
            for (int i = 0; i < templateList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    grid.Children.Add(templateList[i], 0, i / 2);
                }

                else
                {
                    grid.Children.Add(templateList[i], 1, i / 2);
                }

            }
            var scroll = new ScrollView()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Padding = 5
            };
            scroll.Content = grid;
            StackContent.Children.Insert(0, scroll);
        }
        private void InitExerciseList()
        {
            var VM = ((MuscleGroupPageViewModel)BindingContext);
            if (!VM.IsExerciseListView)
            {
                if (ExerciseListView != null)
                {
                    StackContent.Children.Remove(ExerciseListView);
                }
                ExerciseListView = new ListView()
                {
                    Header = new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 30,
                        TextColor = Color.Black,
                        Text = "Gyakorlatok:",
                        Margin = new Thickness(5)
                    },
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    ItemsSource = VM.SelectedMuscleGroup.Exercises,
                    SeparatorVisibility = SeparatorVisibility.None,
                    Footer = new Label()
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Text = "Darabszám : " + VM.SelectedMuscleGroup.Exercises.Count.ToString(),
                        FontSize = 20,
                        Margin = new Thickness(5),
                        TextColor = Color.Black
                    },
                    ItemTemplate = new DataTemplate(() =>
                    {
                        return new ViewCell { View = new ExerciseTemplate() };
                    }),
                };
                switch (Device.RuntimePlatform)
                {
                    case (Device.Android):
                        ExerciseListView.RowHeight = 80; break;
                }
                ExerciseListView.SetBinding(ListView.IsVisibleProperty, nameof(VM.IsExerciseListView));
                ExerciseListView.ItemSelected += ExerciseClickedAsync;
                StackContent.Children.Add(ExerciseListView, 0, 0);
            }
        }

        private async void ExerciseClickedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            if (e.SelectedItem is Exercise exercise)
                await Navigation.PushPopupAsync(new ExerciseDescriptionPopup(exercise));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var VM = ((MuscleGroupPageViewModel)BindingContext);
            VM.IsExerciseListView = false;
        }
        protected override bool OnBackButtonPressed()
        {
            var VM = ((MuscleGroupPageViewModel)BindingContext);
            if (!VM.IsExerciseListView)
                return base.OnBackButtonPressed();
            else
            {
                VM.IsExerciseListView = false;
                return true;
            }
        }
    }
}
