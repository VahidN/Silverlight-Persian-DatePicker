using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfPersianDatePicker.Views
{
    /// <summary>
    /// PDatePicker's view class
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public partial class PDatePicker
    {
        #region Fields (4)

        /// <summary>
        /// Selected Gregorian Date.
        /// </summary>
        public static readonly DependencyProperty SelectedDateProperty =
                DependencyProperty.Register("SelectedDate",
                typeof(string),
                typeof(PDatePicker),
                new PropertyMetadata(string.Empty));
        /// <summary>
        /// Selected Persian Date.
        /// </summary>
        public static readonly DependencyProperty SelectedPersianDateProperty =
                DependencyProperty.Register("SelectedPersianDate",
                typeof(string),
                typeof(PDatePicker),
                new PropertyMetadata(string.Empty));
        /// <summary>
        /// Height of the TextBox.
        /// </summary>
        public static readonly DependencyProperty TextBoxHeightProperty =
                DependencyProperty.Register(
                "TextBoxHeight",
                typeof(int),
                typeof(PDatePicker),
                new PropertyMetadata(24));
        /// <summary>
        /// Width of the TextBox.
        /// </summary>
        public static readonly DependencyProperty TextBoxWidthProperty =
                DependencyProperty.Register(
                "TextBoxWidth",
                typeof(int),
                typeof(PDatePicker),
                new PropertyMetadata(100));

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initialization point.
        /// </summary>
        public PDatePicker()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            //you can listen to handled events too.
            dateTextBox.AddHandler(
                MouseLeftButtonDownEvent,
                new MouseButtonEventHandler(dateTextBoxMouseLeftButtonDown),
                true);
        }

        #endregion Constructors

        #region Properties (4)

        /// <summary>
        /// Selected Gregorian Date.
        /// </summary>
        public string SelectedDate
        {
            get { return (string)GetValue(SelectedDateProperty); }
            set
            {
                pcal1.SelectedDate = value;
                SetValue(SelectedDateProperty, value);
                persianCalnedarPopup.IsOpen = false;
            }
        }

        /// <summary>
        /// Selected Persian Date.
        /// </summary>
        public string SelectedPersianDate
        {
            get { return (string)GetValue(SelectedPersianDateProperty); }
            set
            {
                //todo: validation
                pcal1.SelectedPersianDate = value;
                SetValue(SelectedPersianDateProperty, value);
                persianCalnedarPopup.IsOpen = false;
            }
        }

        /// <summary>
        /// Height of the TextBox.
        /// </summary>
        public int TextBoxHeight
        {
            get { return (int)GetValue(TextBoxHeightProperty); }
            set { SetValue(TextBoxHeightProperty, value); }
        }

        /// <summary>
        /// Width of the TextBox.
        /// </summary>
        public int TextBoxWidth
        {
            get { return (int)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        #endregion Properties

        #region Methods (5)

        // Private Methods (5) 

        private void beginAnimation()
        {            
            var stBoard = pcal1.Resources["FlipAnim1"] as Storyboard;
            if (stBoard == null) return;
            stBoard.Begin();
        }

        private void dateTextBoxMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            showPopup();
        }

        private void dateTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (persianCalnedarPopup.IsOpen)
                persianCalnedarPopup.IsOpen = false;
            dateTextBox.Focus();
        }

        private void openCalendarButtonClick(object sender, RoutedEventArgs e)
        {
            showPopup();
        }

        private void showPopup()
        {
            if (persianCalnedarPopup.IsOpen) return;
            persianCalnedarPopup.IsOpen = true;
            beginAnimation();
        }

        #endregion Methods
    }
}
