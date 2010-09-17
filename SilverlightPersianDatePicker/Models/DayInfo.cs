using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SilverlightPersianDatePicker.Models
{
    /// <summary>
    /// Calendar's cells info model.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public class DayInfo : INotifyPropertyChanged
    {
        #region Fields (8)

        private Brush _backgroundBrush;
        private Brush _borderBrush;
        private int _cellHeight = 22;
        private int _cellWidth = 30;
        private FontWeight _fontWeight;
        private Brush _foreground;
        private bool _hyperlinkIsEnabled = true;
        private string _number;

        #endregion Fields

        #region Properties (8)

        /// <summary>
        /// BackgroundBrush of a cell.
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set
            {
                _backgroundBrush = value;
                raisePropertyChanged("BackgroundBrush");
            }
        }

        /// <summary>
        /// BorderBrush of a cell.
        /// </summary>
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                raisePropertyChanged("BorderBrush");
            }
        }

        /// <summary>
        /// Height of a cell.
        /// </summary>
        public int CellHeight
        {
            get { return _cellHeight; }
            set
            {
                _cellHeight = value;
                raisePropertyChanged("CellHeight");
            }
        }

        /// <summary>
        /// Width of a cell.
        /// </summary>
        public int CellWidth
        {
            get { return _cellWidth; }
            set
            {
                _cellWidth = value;
                raisePropertyChanged("CellWidth");
            }
        }

        /// <summary>
        /// FontWeight of a cell's Text.
        /// </summary>
        public FontWeight FontWeight
        {
            get
            {
                return _fontWeight;
            }
            set
            {
                _fontWeight = value;
                raisePropertyChanged("FontWeight");
            }
        }

        /// <summary>
        /// Foreground of a cell's Text.
        /// </summary>
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                raisePropertyChanged("Foreground");
            }
        }

        /// <summary>
        /// Is Hyperlink of a cell enabled?
        /// </summary>
        public bool HyperlinkIsEnabled
        {
            get { return _hyperlinkIsEnabled; }
            set
            {
                _hyperlinkIsEnabled = value;
                raisePropertyChanged("HyperlinkIsEnabled");
            }
        }

        /// <summary>
        /// Text of a cell.
        /// </summary>
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                raisePropertyChanged("Number");
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
