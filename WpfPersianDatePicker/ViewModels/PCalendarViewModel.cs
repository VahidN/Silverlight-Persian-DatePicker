using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfPersianDatePicker.Brushes;
using WpfPersianDatePicker.DateHelper;
using WpfPersianDatePicker.Models;
using WpfPersianDatePicker.MVVMHelper;

namespace WpfPersianDatePicker.ViewModels
{
    /// <summary>
    /// Persian CalendarViewModel class.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>
    public class PCalendarViewModel : INotifyPropertyChanged
    {
        #region Fields (8)

        private CalendarGUI _calendarGuiData;
        private int _currentMonth;
        private int _currentYear;
        private ObservableCollection<DayInfo> _daysInfo;
        private int _inc;
        int _lastMonthIndex = -1;
        private int _lastSelectedIdx = -1;
        int _lastYear = -1;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initialization point.
        /// </summary>
        public PCalendarViewModel()
        {
            CalendarGUIData = new CalendarGUI();
            calendarMode = CalendarMode.Month;
            DaysInfo = new ObservableCollection<DayInfo>();
            setupCommands();
            initHeader();
            setToday();
        }

        #endregion Constructors

        #region Properties (8)

        /// <summary>
        /// Notifies GUI about the changes in CalendarGUIData.
        /// </summary>
        public CalendarGUI CalendarGUIData
        {
            get { return _calendarGuiData; }
            set
            {
                _calendarGuiData = value;
                raisePropertyChanged("CalendarGUIData");
            }
        }

        private CalendarMode calendarMode { set; get; }

        /// <summary>
        /// ClickNext event handler.
        /// </summary>
        public ICommand ClickNext { set; get; }

        /// <summary>
        /// ClickPrev event handler.
        /// </summary>
        public ICommand ClickPrev { set; get; }

        /// <summary>
        /// DayClick event handler.
        /// </summary>
        public ICommand DayClick { set; get; }

        /// <summary>
        /// Notifies GUI about current view data of the calendar.
        /// </summary>
        public ObservableCollection<DayInfo> DaysInfo
        {
            get
            {
                return _daysInfo;
            }
            set
            {
                _daysInfo = value;
                raisePropertyChanged("DaysInfo");
            }
        }

        /// <summary>
        /// ShowTodayClick event handler.
        /// </summary>
        public ICommand ShowTodayClick { set; get; }

        /// <summary>
        /// TitleClick event handler.
        /// </summary>
        public ICommand TitleClick { set; get; }

        #endregion Properties

        #region Delegates and Events (2)

        // Delegates (1)

        /// <summary>
        /// Our custom delegate for starting an animation after each update.
        /// </summary>
        public delegate void StartAnimationEventHandler();
        // Events (1)

        /// <summary>
        /// StartAnimation Event.
        /// </summary>
        public event StartAnimationEventHandler StartAnimation;

        #endregion Delegates and Events

        #region Methods (24)

        // Public Methods (1)

        /// <summary>
        /// Selecting a day from view.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public void SelectThisDay(int year, int month, int day)
        {
            getInfoSetCalendar(month, year);
            //select that day
            highlightThisday(year, month, day);
        }
        // Private Methods (23)

        private static bool canClickNext(string obj)
        {
            return true;
        }

        private static bool canClickPrev(string obj)
        {
            return true;
        }

        private static bool canDayClick(string day)
        {
            return true;
        }

        private static bool canShowTodayClick(string obj)
        {
            return true;
        }

        private static bool canTitleClick(string obj)
        {
            return true;
        }

        private void clickNext(string obj)
        {
            switch (calendarMode)
            {
                case CalendarMode.Month:
                    {
                        if (_currentMonth + 1 == 12)
                        {
                            getInfoSetCalendar(1, _currentYear + 1);
                        }
                        else
                        {
                            _inc = 1;
                            getInfoSetCalendar(_currentMonth + 1 + _inc, _currentYear);
                        }
                    }
                    break;
                case CalendarMode.Year:
                    {
                        _currentYear++;
                        CalendarGUIData.HeaderText = _currentYear.ToString();
                    }
                    break;
                case CalendarMode.Decade:
                    {
                        _currentYear += 12;
                        CalendarGUIData.HeaderText = _currentYear.ToString();
                        showDecade();
                    }
                    break;
            }
        }

        private void clickPrev(string obj)
        {
            switch (calendarMode)
            {
                case CalendarMode.Month:
                    {
                        if (_currentMonth + 1 == 1)
                        {
                            getInfoSetCalendar(12, _currentYear - 1);
                        }
                        else
                        {
                            _inc = -1;
                            getInfoSetCalendar(_currentMonth + 1 + _inc, _currentYear);
                        }
                    }
                    break;
                case CalendarMode.Year:
                    {
                        _currentYear--;
                        CalendarGUIData.HeaderText = _currentYear.ToString();
                    }
                    break;
                case CalendarMode.Decade:
                    {
                        _currentYear -= 12;
                        CalendarGUIData.HeaderText = _currentYear.ToString();
                        showDecade();
                    }
                    break;
            }
        }

        private void dayClick(string day)
        {
            if (string.IsNullOrWhiteSpace(day)) return;

            //click on the header
            if (Names.DaysOfWeek.Contains(day)) return;

            switch (calendarMode)
            {
                case CalendarMode.Month:
                    {
                        var dd = int.Parse(day);
                        var month = _currentMonth + 1;
                        var year = _currentYear;

                        setTodayData(year, month, dd);
                        highlightThisday(year, month, dd);
                    }
                    break;
                case CalendarMode.Year:
                    {
                        var monthIdx = 0;
                        for (var i = 0; i < 12; i++)
                        {
                            if (Names.MonthNames[i] != day) continue;
                            monthIdx = i;
                            break;
                        }
                        DaysInfo.Clear();
                        initHeader();
                        getInfoSetCalendar(monthIdx + 1, _currentYear);
                    }
                    break;
                case CalendarMode.Decade:
                    {
                        _currentYear = int.Parse(day);
                        showYear();
                    }
                    break;
            }

        }

        private void getInfoSetCalendar(int monthIndex, int year)
        {
            //saves some cpu cycles...
            if (monthIndex == _lastMonthIndex && year == _lastYear) return;
            //cache
            _lastMonthIndex = monthIndex;
            _lastYear = year;

            calendarMode = CalendarMode.Month;
            _currentYear = year;
            _currentMonth = monthIndex - 1;
            CalendarGUIData.HeaderText = string.Format("{0} {1}", Names.MonthNames[monthIndex - 1], year);

            for (var i = 0; i <= 41; i++)
            {
                DaysInfo[i + 7].FontWeight = FontWeights.Normal;
            }

            var dayWeek = PDateHelper.Find1StDayOfMonth(year, monthIndex);

            var noOfDays = monthIndex <= 6 ? 31 : 30;

            if (_currentMonth == 11)
                noOfDays = PDateHelper.IsLeapYear(year) ? 30 : 29;

            persianCalendar(dayWeek, noOfDays);

            highlightToday(monthIndex, year);
            if (StartAnimation != null) StartAnimation();
        }

        private void highlightThisday(int year, int month, int day)
        {
            if (_currentMonth + 1 != month || _currentYear != year) return;

            //reset
            if (_lastSelectedIdx != -1)
            {
                DaysInfo[_lastSelectedIdx].BackgroundBrush = CustomBrushes.BorderBlueBackground();
                DaysInfo[_lastSelectedIdx].BorderBrush = CustomBrushes.CellsBorederBrush();
                DaysInfo[_lastSelectedIdx].Foreground = CustomBrushes.CellsForeground();
                DaysInfo[_lastSelectedIdx].FontWeight = FontWeights.Normal;
            }

            for (var i = 0; i <= 41; i++)
            {
                if (DaysInfo[i + 7].Number != day.ToString())
                {
                    continue;
                }
                DaysInfo[i + 7].FontWeight = FontWeights.Bold;
                DaysInfo[i + 7].BackgroundBrush = CustomBrushes.HighlightBackground();
                DaysInfo[i + 7].BorderBrush = CustomBrushes.HightlightBorderBrush();
                _lastSelectedIdx = i + 7;
            }
        }

        private void highlightToday(int monthIndex, int year)
        {
            int tOutYear, tOutMonth, tOutDay;
            PDateHelper.GregorianToHijri(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                out tOutYear, out tOutMonth, out tOutDay);

            if ((year != tOutYear) || (monthIndex != tOutMonth)) return;
            //highlight today!
            for (var i = 0; i <= 41; i++)
            {
                if (DaysInfo[i + 7].Number != tOutDay.ToString()) continue;
                DaysInfo[i + 7].FontWeight = FontWeights.Bold;
                DaysInfo[i + 7].BackgroundBrush = CustomBrushes.TodayBackground();
                DaysInfo[i + 7].BorderBrush = CustomBrushes.TodayBorderBrush();
            }
        }

        private void initHeader()
        {
            CalendarGUIData.UniformGridRows = 5;
            CalendarGUIData.UniformGridColumns = 7;

            foreach (var day in Names.DaysOfWeek)
            {
                DaysInfo.Add(new DayInfo
                {
                    Number = day,
                    FontWeight = FontWeights.Bold,
                    BackgroundBrush = CustomBrushes.HeaderBackground(),
                    BorderBrush = CustomBrushes.HeaderBorderBrush(),
                    Foreground = CustomBrushes.HeaderForeground(),
                    CellWidth = 30,
                    HyperlinkIsEnabled = true
                });
            }

            for (var i = 0; i <= 41; i++)
            {
                DaysInfo.Add(new DayInfo { CellWidth = 30 });
            }
        }

        private void persianCalendar(int startDay, int nDays)
        {
            int i, j;

            reset();

            for (i = startDay; i <= 6; i++)
            {
                var curDay = i - startDay + 1;
                DaysInfo[i + 7].Number = curDay.ToString();
                DaysInfo[i + 7].BackgroundBrush = CustomBrushes.BorderBlueBackground();
                DaysInfo[i + 7].BorderBrush = CustomBrushes.CellsBorederBrush();
                DaysInfo[i + 7].Foreground = CustomBrushes.CellsForeground();
            }

            var k = 7;
            for (j = 6 - startDay + 1; j <= nDays - 1; j++)
            {
                var curDay = j + 1;
                DaysInfo[k + 7].Number = curDay.ToString();
                DaysInfo[k + 7].BackgroundBrush = CustomBrushes.BorderBlueBackground();
                DaysInfo[k + 7].BorderBrush = CustomBrushes.CellsBorederBrush();
                DaysInfo[k + 7].Foreground = CustomBrushes.CellsForeground();
                k = k + 1;
            }
        }

        private void reset()
        {
            _lastSelectedIdx = -1;
            for (var i = 0; i <= 41; i++)
            {
                DaysInfo[i + 7] = new DayInfo();
            }
        }

        private void resetTheCache()
        {
            _lastMonthIndex = -1;
            _lastYear = -1;
        }

        private void setToday()
        {
            int outYear, outMonth, outDay;
            PDateHelper.GregorianToHijri(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                out outYear, out outMonth, out outDay);

            CalendarGUIData.Today = string.Format("{0}/{1}/{2}", outYear, outMonth.ToString("00"), outDay.ToString("00"));
            setTodayData(outYear, outMonth, outDay);

            if (_currentYear == 0)
            {
                getInfoSetCalendar(outMonth, outYear);
            }
        }

        private void setTodayData(int year, int month, int day)
        {
            resetGui();

            var pDate = string.Format("{0}/{1}/{2}", year, month, day);
            CalendarGUIData.SelectedPersianDate = pDate;

            int outYear, outMonth, outDay;
            PDateHelper.HijriToGregorian(year, month, day, out outYear, out outMonth, out outDay);
            var gDate = new DateTime(outYear, outMonth, outDay);
            CalendarGUIData.SelectedDate = gDate;
            raisePropertyChanged("CalendarGUIData");
        }

        private void resetGui()
        {
            CalendarGUIData.SelectedPersianDate = string.Empty;
            CalendarGUIData.SelectedDate = null;
            raisePropertyChanged("CalendarGUIData");
        }

        private void setupCommands()
        {
            DayClick = new DelegateCommand<string>(dayClick, canDayClick);
            ClickPrev = new DelegateCommand<string>(clickPrev, canClickPrev);
            ClickNext = new DelegateCommand<string>(clickNext, canClickNext);
            TitleClick = new DelegateCommand<string>(titleClick, canTitleClick);
            ShowTodayClick = new DelegateCommand<string>(showTodayClick, canShowTodayClick);
        }

        private void showDecade()
        {
            DaysInfo.Clear();

            CalendarGUIData.UniformGridColumns = 3;
            CalendarGUIData.UniformGridRows = 4;

            for (var i = 0; i < 12; i++)
            {
                DaysInfo.Add(new DayInfo
                {
                    Number = (_currentYear - i).ToString(),
                    FontWeight = FontWeights.Bold,
                    BackgroundBrush = CustomBrushes.HighlightBackground(),
                    BorderBrush = CustomBrushes.HightlightBorderBrush(),
                    Foreground = CustomBrushes.HightlightForeground(),
                    CellWidth = 60,
                    CellHeight = 40
                });
            }

            calendarMode = CalendarMode.Decade;
            if (StartAnimation != null) StartAnimation();
        }

        private void showMonth()
        {
            DaysInfo.Clear();
            initHeader();
            if (_currentMonth < 1)
                _currentMonth = 1;
            else
                _currentMonth++;
            getInfoSetCalendar(_currentMonth, _currentYear);
            calendarMode = CalendarMode.Month;
        }

        private void showTodayClick(string obj)
        {
            resetTheCache();

            int outYear, outMonth, outDay;
            PDateHelper.GregorianToHijri(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                out outYear, out outMonth, out outDay);
            DaysInfo.Clear();
            initHeader();
            getInfoSetCalendar(outMonth, outYear);
            if (StartAnimation != null) StartAnimation();
        }

        private void showYear()
        {
            CalendarGUIData.HeaderText = _currentYear.ToString();
            DaysInfo.Clear();

            CalendarGUIData.UniformGridColumns = 3;
            CalendarGUIData.UniformGridRows = 4;

            for (var i = 0; i < 12; i++)
            {
                DaysInfo.Add(new DayInfo
                {
                    Number = Names.MonthNames[i],
                    FontWeight = FontWeights.Bold,
                    BackgroundBrush = CustomBrushes.HeaderBackground(),
                    BorderBrush = CustomBrushes.HeaderBorderBrush(),
                    Foreground = CustomBrushes.HeaderForeground(),
                    CellWidth = 60,
                    CellHeight = 40
                });
            }

            calendarMode = CalendarMode.Year;
            if (StartAnimation != null) StartAnimation();
        }

        private void titleClick(string obj)
        {
            resetTheCache();

            switch (calendarMode)
            {
                case CalendarMode.Month:
                    showYear();
                    break;
                case CalendarMode.Year:
                    showDecade();
                    break;
                case CalendarMode.Decade:
                    showMonth();
                    break;
            }
        }

        #endregion Methods



        #region INotifyPropertyChanged Members
        /// <summary>
        /// PropertyChanged notification.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        void raisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
