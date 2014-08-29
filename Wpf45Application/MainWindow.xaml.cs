using System.Windows;

namespace Wpf45Application
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window1();
            win.Show();
            //win.ShowDialog();
        }
    }
}