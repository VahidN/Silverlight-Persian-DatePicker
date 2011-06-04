using System;
using System.Windows;

namespace SilverlightPersianDatePickerUsage
{
    public partial class NormalTest
    {
        public NormalTest()
        {
            InitializeComponent();
            this.Loaded += NormalTest_Loaded;
        }

        void NormalTest_Loaded(object sender, RoutedEventArgs e)
        {
            PDatePicker1.SelectedDate = DateTime.Now.ToString();
            PDatePicker2.SelectedPersianDate = "1390/03/10";
        }
    }
}
