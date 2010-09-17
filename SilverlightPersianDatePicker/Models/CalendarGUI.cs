using System.ComponentModel;
using System;

namespace SilverlightPersianDatePicker.Models
{
    /// <summary>
    /// General info's of a calendar's page.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public class CalendarGUI : INotifyPropertyChanged
    {
        #region Fields (6)

        private string _headerText;
        private DateTime _selectedDate;
        private string _selectedPersianDate;
        private string _today;
        private int _uniformGridColumns = 7;
        private int _uniformGridRows = 5;

        #endregion Fields

        #region Properties (6)

        /// <summary>
        /// HeaderText of the calendar.
        /// </summary>
        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                raisePropertyChanged("HeaderText");
            }
        }

        /// <summary>
        /// SelectedDate in Gregorian format.
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                raisePropertyChanged("SelectedDate");
            }
        }

        /// <summary>
        /// SelectedDate in Persian format.
        /// </summary>
        public string SelectedPersianDate
        {
            get { return _selectedPersianDate; }
            set
            {
                _selectedPersianDate = value;
                raisePropertyChanged("SelectedPersianDate");
            }
        }

        /// <summary>
        /// Shows today's string.
        /// </summary>
        public string Today
        {
            get { return _today; }
            set
            {
                _today = value;
                raisePropertyChanged("Today");
            }
        }

        /// <summary>
        /// Number of UniformGrid's Columns.
        /// </summary>
        public int UniformGridColumns
        {
            get { return _uniformGridColumns; }
            set
            {
                _uniformGridColumns = value;
                raisePropertyChanged("UniformGridColumns");
            }
        }

        /// <summary>
        /// Number of UniformGrid's Rows.
        /// </summary>
        public int UniformGridRows
        {
            get { return _uniformGridRows; }
            set
            {
                _uniformGridRows = value;
                raisePropertyChanged("UniformGridRows");
            }
        }

        #endregion Properties



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
