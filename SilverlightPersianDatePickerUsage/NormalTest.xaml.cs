using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SilverlightPersianDatePickerUsage
{
    public partial class NormalTest
    {
        public NormalTest()
        {
            InitializeComponent();
            this.Loaded += NormalTest_Loaded;
            RegisterForNotification(this.PDatePicker1, "SelectedPersianDate",
                (d, e) =>
                {
                    //Do something ...
                });
        }

        public void RegisterForNotification(FrameworkElement element, string propertyName, PropertyChangedCallback callback)
        {
            var b = new Binding(propertyName) { Source = element };
            var prop = DependencyProperty.RegisterAttached(
                "ListenAttached" + propertyName,
                typeof(object),
                typeof(UserControl),
                new PropertyMetadata(callback));
            element.SetBinding(prop, b);
        }

        void NormalTest_Loaded(object sender, RoutedEventArgs e)
        {
            PDatePicker1.SelectedDate = DateTime.Now.ToString();
            PDatePicker2.SelectedPersianDate = "1390/03/10";
        }
    }
}
