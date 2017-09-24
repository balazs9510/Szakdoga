using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.Common;
using WorkoutPlaner.EventArgs;
using Xamarin.Forms;

namespace WorkoutPlaner.Controls
{
    public class CheckBox : View
    {
        /// <summary>
		/// The checked state property.
		/// </summary>
		public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CheckBox), false,
                defaultBindingMode: BindingMode.TwoWay,
                 propertyChanged: OnCheckedChanged);
        /// <summary>Image property for checkbox background </summary>
        public static readonly BindableProperty BoxColorProperty =
            BindableProperty.Create(nameof(BoxColor), typeof(Color), typeof(CheckBox), Color.Default);
        /// <summary>
        /// Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkBox = (CheckBox)bindable;
            checkBox.Checked = (bool)newValue;
            MessagingCenter.Send(checkBox,MessagingKeys.DoneExerciseCheckChanged,checkBox.BindingContext);
        }

        /// <summary>
        /// The checked text property.
        /// </summary>
        public static readonly BindableProperty CheckedTextProperty =
            BindableProperty.Create(nameof(CheckedText), typeof(string), typeof(CheckBox), string.Empty, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// The unchecked text property.
        /// </summary>
        public static readonly BindableProperty UncheckedTextProperty =
            BindableProperty.Create(nameof(UncheckedText), typeof(string), typeof(CheckBox), string.Empty, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// The default text property.
        /// </summary>
        public static readonly BindableProperty DefaultTextProperty =
            BindableProperty.Create(nameof(DefaultText), typeof(string), typeof(CheckBox), string.Empty, defaultBindingMode: BindingMode.TwoWay);
        /// <summary>
        /// Identifies the TextColor bindable property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
                       BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBox), Color.Default);
        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
                       BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckBox), 11.0, defaultBindingMode: BindingMode.TwoWay);
        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
                      BindableProperty.Create(nameof(FontName), typeof(string), typeof(CheckBox), "Matthiola.ttf", defaultBindingMode: BindingMode.TwoWay);


        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }

            set
            {
                if (this.Checked != value)
                {
                    this.SetValue(CheckedProperty, value);
                }
            }
        }
        public Color BoxColor
        {
            get
            {
                return (Color)GetValue(BoxColorProperty);
            }
            set
            {
                this.SetValue(BoxColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the checked text.
        /// </summary>
        /// <value>The checked state.</value>
        /// <remarks>
        /// Overwrites the default text property if set when checkbox is checked.
        /// </remarks>
        public string CheckedText
        {
            get
            {
                return (string)GetValue(CheckedTextProperty);
            }

            set
            {
                this.SetValue(CheckedTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        /// <remarks>
        /// Overwrites the default text property if set when checkbox is checked.
        /// </remarks>
        public string UncheckedText
        {
            get
            {
                return (string)GetValue(UncheckedTextProperty);
            }

            set
            {
                this.SetValue(UncheckedTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string DefaultText
        {
            get
            {
                return (string)GetValue(DefaultTextProperty);
            }

            set
            {
                this.SetValue(DefaultTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                this.SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName
        {
            get
            {
                return (string)GetValue(FontNameProperty);
            }
            set
            {
                SetValue(FontNameProperty, value);
            }
        }
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return this.Checked
                    ? (string.IsNullOrEmpty(this.CheckedText) ? this.DefaultText : this.CheckedText)
                        : (string.IsNullOrEmpty(this.UncheckedText) ? this.DefaultText : this.UncheckedText);
            }
        }
    }
}
