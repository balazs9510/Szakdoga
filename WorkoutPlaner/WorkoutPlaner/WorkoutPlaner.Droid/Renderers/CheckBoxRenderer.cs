using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WorkoutPlaner.Controls;
using WorkoutPlaner.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckBoxRenderer))]
namespace WorkoutPlaner.Droid.Renderers
{
    public class CheckBoxRenderer : ViewRenderer<CheckBox, Android.Widget.CheckBox>
    {
        private ColorStateList defaultTextColor;

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                var checkBox = new Android.Widget.CheckBox(this.Context)
                {
                    Text = e.NewElement.Text,
                    Checked = e.NewElement.Checked,

                };
                checkBox.Visibility = e.NewElement.IsVisible ? Android.Views.ViewStates.Visible
                    : Android.Views.ViewStates.Invisible;
                checkBox.Enabled = e.NewElement.IsEnabled;
                checkBox.CheckedChange += CheckBoxCheckedChange;

                defaultTextColor = checkBox.TextColors;
                SetNativeControl(checkBox);
            }
            int[][] states = {
                    new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
                    new int[] {Android.Resource.Attribute.StateEnabled}, // disabled
                    new int[] {Android.Resource.Attribute.StateChecked}, // unchecked
                    new int[] { Android.Resource.Attribute.StatePressed}  // pressed
                };
            var checkBoxColor = (int)e.NewElement.BoxColor.ToAndroid();
            int[] colors = {
                   checkBoxColor,
                    checkBoxColor,
                    checkBoxColor,
                    checkBoxColor
                };
            var myList = new ColorStateList(states, colors);
            Control.ButtonTintList = myList;

            //Control.SetHeight(50);
            //Control.SetWidth(50);
            // SetTheme(Control, 5, 50, 5, e.NewElement.BoxColor.ToAndroid());
            UpdateTextColor();
            Control.SetPadding(10, 5, 5, 5);
            if (e.NewElement.FontSize > 0)
            {
                Control.TextSize = (float)e.NewElement.FontSize;
            }

            if (!string.IsNullOrEmpty(e.NewElement.FontName))
            {
                Control.Typeface = TrySetFont(e.NewElement.FontName);
            }
        }
        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Text = Element.Text;
                    Control.Checked = Element.Checked;
                    break;
                case "TextColor":
                    UpdateTextColor();
                    break;
                case "FontName":
                    if (!string.IsNullOrEmpty(Element.FontName))
                    {
                        Control.Typeface = TrySetFont(Element.FontName);
                    }
                    break;
                case "FontSize":
                    if (Element.FontSize > 0)
                    {
                        Control.TextSize = (float)Element.FontSize;
                    }
                    break;
                case "CheckedText":
                case "UncheckedText":
                    Control.Text = Element.Text;
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
                    break;
            }
        }

        /// <summary>
        /// CheckBoxes the checked change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Android.Widget.CompoundButton.CheckedChangeEventArgs"/> instance containing the event data.</param>
        void CheckBoxCheckedChange(object sender, Android.Widget.CompoundButton.CheckedChangeEventArgs e)
        {
            this.Element.Checked = e.IsChecked;
        }

        /// <summary>
        /// Tries the set font.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <returns>Typeface.</returns>
        private Typeface TrySetFont(string fontName)
        {
            Typeface tf = Typeface.Default;
            try
            {
                tf = Typeface.CreateFromAsset(Context.Assets, fontName);
                return tf;
            }
            catch (Exception ex)
            {
                Console.Write("not found in assets {0}", ex);
                try
                {
                    tf = Typeface.CreateFromFile(fontName);
                    return tf;
                }
                catch (Exception ex1)
                {
                    Console.Write(ex1);
                    return Typeface.Default;
                }
            }
        }

        /// <summary>
        /// Updates the color of the text
        /// </summary>
        private void UpdateTextColor()
        {
            if (Control == null || Element == null)
                return;

            if (Element.TextColor == Xamarin.Forms.Color.Default)
                Control.SetTextColor(defaultTextColor);
            else
                Control.SetTextColor(Element.TextColor.ToAndroid());
        }
        public static void SetTheme(Android.Widget.CheckBox checkBox,
            int radius, int size, int border,Android.Graphics.Color background)
        {
            // creating unchecked-enabled state drawable
            GradientDrawable uncheckedEnabled = new GradientDrawable();
            uncheckedEnabled.SetCornerRadius(radius);
            uncheckedEnabled.SetSize(size, size);
            uncheckedEnabled.SetColor(Android.Graphics.Color.Transparent);
            uncheckedEnabled.SetStroke(border, Android.Graphics.Color.Black);

            // creating checked-enabled state drawable
            GradientDrawable checkedOutside = new GradientDrawable();
            checkedOutside.SetCornerRadius(radius);
            checkedOutside.SetSize(size, size);
            checkedOutside.SetColor(Android.Graphics.Color.Transparent);
            checkedOutside.SetStroke(border, Android.Graphics.Color.Black);

            PaintDrawable checkedCore = new PaintDrawable(background);
            checkedCore.SetCornerRadius(radius);
            checkedCore.SetIntrinsicHeight(size);
            checkedCore.SetIntrinsicWidth(size);
            InsetDrawable checkedInside = new InsetDrawable(checkedCore, border + 2, border + 2, border + 2, border + 2);

            Drawable[] checkedEnabledDrawable = { checkedOutside, checkedInside };
            LayerDrawable checkedEnabled = new LayerDrawable(checkedEnabledDrawable);

            // creating unchecked-enabled state drawable
            GradientDrawable uncheckedDisabled = new GradientDrawable();
            uncheckedDisabled.SetCornerRadius(radius);
            uncheckedDisabled.SetSize(size, size);
            uncheckedDisabled.SetColor(Android.Graphics.Color.Transparent);
            uncheckedDisabled.SetStroke(border, Android.Graphics.Color.Black);

            // creating checked-disabled state drawable
            GradientDrawable checkedOutsideDisabled = new GradientDrawable();
            checkedOutsideDisabled.SetCornerRadius(radius);
            checkedOutsideDisabled.SetSize(size, size);
            checkedOutsideDisabled.SetColor(Android.Graphics.Color.Transparent);
            checkedOutsideDisabled.SetStroke(border, Android.Graphics.Color.Black);

            PaintDrawable checkedCoreDisabled = new PaintDrawable(background);
            checkedCoreDisabled.SetCornerRadius(radius);
            checkedCoreDisabled.SetIntrinsicHeight(size);
            checkedCoreDisabled.SetIntrinsicWidth(size);
            InsetDrawable checkedInsideDisabled = new InsetDrawable(checkedCoreDisabled, border + 2, border + 2, border + 2, border + 2);

            Drawable[] checkedDisabledDrawable = { checkedOutsideDisabled, checkedInsideDisabled };
            LayerDrawable checkedDisabled = new LayerDrawable(checkedDisabledDrawable);


            StateListDrawable states = new StateListDrawable();
            states.AddState(new int[] { -Android.Resource.Attribute.StateChecked, Android.Resource.Attribute.StateEnabled }, uncheckedEnabled);
            states.AddState(new int[] { Android.Resource.Attribute.StateChecked, Android.Resource.Attribute.StateEnabled }, checkedEnabled);
            states.AddState(new int[] { -Android.Resource.Attribute.StateChecked, -Android.Resource.Attribute.StateEnabled }, uncheckedDisabled);
            states.AddState(new int[] { Android.Resource.Attribute.StateChecked, -Android.Resource.Attribute.StateEnabled }, checkedDisabled);
            checkBox.SetButtonDrawable(states);

            // setting padding for avoiding text to be appear on icon
            checkBox.SetPadding(size / 4 * 5, 0, 0, 0);
            checkBox.SetTextColor(Android.Graphics.Color.LightYellow);
            checkBox.SetTextSize(Android.Util.ComplexUnitType.Sp, 10);
            checkBox.SetTypeface(Typeface.Default, TypefaceStyle.Normal);
        }
    }
}