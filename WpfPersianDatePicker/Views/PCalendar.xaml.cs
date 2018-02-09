using System;
using System.ComponentModel;
using System.Windows;
using WpfPersianDatePicker.DateHelper;
using WpfPersianDatePicker.ViewModels;

namespace WpfPersianDatePicker.Views
{
    /// <summary>
    /// Calendar's view class.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>
    public partial class PCalendar
    {
        #region Fields (3)

        private readonly PCalendarViewModel _calendarViewModel;

        /// <summary>
        /// Selected Gregorian Date.
        /// </summary>
        public static readonly DependencyProperty SelectedDateProperty =
                DependencyProperty.Register("SelectedDate",
                typeof(string),
                typeof(PCalendar),
                new PropertyMetadata(string.Empty, selectedDateChanged));

        /// <summary>
        /// Selected Persian Date.
        /// </summary>
        public static readonly DependencyProperty SelectedPersianDateProperty =
                DependencyProperty.Register("SelectedPersianDate",
                typeof(string),
                typeof(PCalendar),
                new PropertyMetadata(string.Empty, selectedPersianDateChanged));

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initialization point.
        /// </summary>
        public PCalendar()
        {
            InitializeComponent();
            _calendarViewModel = new PCalendarViewModel();
            LayoutRoot.DataContext = _calendarViewModel;
            Loaded += pCalendarLoaded;
            Unloaded += pCalendarUnloaded;
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Selected Gregorian Date.
        /// </summary>
        public string SelectedDate
        {
            get
            {
                return (string)GetValue(SelectedDateProperty);
            }
            set
            {
                SetValue(SelectedDateProperty, value);
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
                SetValue(SelectedPersianDateProperty, value);
            }
        }

        #endregion Properties

        #region Methods (7)

        // Private Methods (7)

        void calendarViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CalendarGUIData")
            {
                SelectedDate = _calendarViewModel.CalendarGUIData.SelectedDate?.ToString("yyyy/MM/dd");
                SelectedPersianDate = _calendarViewModel.CalendarGUIData.SelectedPersianDate;
            }
        }

        void pCalendarLoaded(object sender, RoutedEventArgs e)
        {
            _calendarViewModel.PropertyChanged += calendarViewModelPropertyChanged;
        }

        void pCalendarUnloaded(object sender, RoutedEventArgs e)
        {
            _calendarViewModel.PropertyChanged -= calendarViewModelPropertyChanged;
        }

        private static void selectedDateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var pCalendar = o as PCalendar;
            if (pCalendar != null)
                pCalendar.setSelectedGDate(e);
        }

        private static void selectedPersianDateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var pCalendar = o as PCalendar;
            if (pCalendar != null)
                pCalendar.setSelectedPDate(e);
        }

        void setSelectedGDate(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                resetDates();
                return;
            }
            var eDate = e.NewValue.ToString();
            if (string.IsNullOrWhiteSpace(eDate))
            {
                resetDates();
                return;
            }

            DateTime? result = e.NewValue.DateTimeTryParse();
            if (!result.HasValue) return;
            var gDate = result.Value;

            //تبديل به تاريخ فارسي
            int year, month, day;
            if (PDateHelper.GregorianToHijri(
                gDate.Year,
                gDate.Month,
                gDate.Day,
                out year, out month, out day))
            {
                SelectedPersianDate = string.Format("{0}/{1}/{2}", year, month, day);
            }
        }

        void setSelectedPDate(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                resetDates();
                return;
            }
            var pDate = e.NewValue.ToString();
            pDate = pDate.ToResilientPersianDate();
            if (string.IsNullOrWhiteSpace(pDate))
            {
                resetDates();
                return;
            }

            var parts = PDateHelper.ExtractPersianDateParts(pDate);
            var year = parts.Item1;
            var month = parts.Item2;
            var day = parts.Item3;
            _calendarViewModel.SelectThisDay(year, month, day);
            synchronize(year, month, day);
        }

        private void synchronize(int year, int month, int day)
        {
            int outYear, outMonth, outDay;
            PDateHelper.HijriToGregorian(year, month, day, out outYear, out outMonth, out outDay);
            SelectedDate = string.Format("{0}/{1}/{2}", outYear, outMonth, outDay);
        }

        private void resetDates()
        {
            SelectedPersianDate = string.Empty;
            SelectedDate = null;
        }

        #endregion Methods
    }
}
