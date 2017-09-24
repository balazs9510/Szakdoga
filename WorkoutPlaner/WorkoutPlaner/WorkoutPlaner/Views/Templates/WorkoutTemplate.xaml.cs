using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.DAL.Model;
using Xamarin.Forms;

namespace WorkoutPlaner.Views.Templates
{
    public partial class WorkoutTemplate : ContentView
    {
        public WorkoutTemplate()
        {
            InitializeComponent();
            var acfs = this.BindingContext;
        }
    }
}