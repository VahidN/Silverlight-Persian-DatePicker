using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WpfPersianDatePicker.Models
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
                if (_backgroundBrush == value) return;
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
                if (_borderBrush == value) return;
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
                if (_cellHeight == value) return;
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
                if (_cellWidth == value) return;
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
                if (_fontWeight == value) return;
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
                if (_foreground == value) return;
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
                if (_hyperlinkIsEnabled == value) return;
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
                if (_number == value) return;
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
