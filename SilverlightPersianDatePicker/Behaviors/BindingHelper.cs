using System.Windows;
using System.Windows.Controls;
using SilverlightPersianDatePicker.DateHelper;

namespace SilverlightPersianDatePicker.Behaviors
{
    public static class BindingHelper
    {
        public static readonly DependencyProperty
                        UpdateSourceOnChangeProperty =
                            DependencyProperty.RegisterAttached(
                                    "UpdateSourceOnChange",
                                    typeof(bool),
                                    typeof(BindingHelper),
                                    new PropertyMetadata(false, OnPropertyChanged));

        public static bool GetUpdateSourceOnChange(DependencyObject obj)
        {
            return (bool)obj.GetValue(UpdateSourceOnChangeProperty);
        }

        public static void SetUpdateSourceOnChange(DependencyObject obj, bool value)
        {
            obj.SetValue(UpdateSourceOnChangeProperty, value);
        }

        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var txt = obj as TextBox;
            if (txt == null)
                return;
            if ((bool)e.NewValue)
            {
                txt.TextChanged += OnTextChanged;
            }
            else
            {
                txt.TextChanged -= OnTextChanged;
            }
        }

        static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null)
                return;

            var be = txt.GetBindingExpression(TextBox.TextProperty);
            if (be != null)
            {
                var selectionStart = txt.SelectionStart;
                txt.Text = txt.Text.ToPersianNumbers();
                be.UpdateSource();
                txt.SelectionStart = selectionStart;
            }
        }
    }
}