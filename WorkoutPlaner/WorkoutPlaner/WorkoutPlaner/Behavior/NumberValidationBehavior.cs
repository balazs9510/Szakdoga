using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace WorkoutPlaner.Behavior
{
    public class NumberValidationBehavior : Behavior<Entry>
    {
        private const string digitRegex = @"^\d+$";
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;

            base.OnAttachedTo(bindable);
        }


        void HandleTextChanged(object sender, TextChangedEventArgs e)

        {
            bool IsValid = false;

            IsValid = (Regex.IsMatch(e.NewTextValue, digitRegex));
            if (string.IsNullOrEmpty(((Entry)sender).Text) || ((Entry)sender).Text.Equals("-"))
                IsValid = true;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }


        protected override void OnDetachingFrom(Entry bindable)

        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

    }
}