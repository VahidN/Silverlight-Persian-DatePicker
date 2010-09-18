using System.Windows.Navigation;

namespace SilverlightPersianDatePickerUsage
{
    public partial class MainMenu
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void frame1NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
