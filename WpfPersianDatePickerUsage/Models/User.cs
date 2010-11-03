using System;
using System.ComponentModel;

namespace WpfPersianDatePickerUsage.Models
{
    public class User : INotifyPropertyChanged
    {
        #region Fields (2)

        DateTime _accountValidTo;
        string _name;

        #endregion Fields

        #region Properties (2)

        public DateTime AccountValidTo
        {
            get { return _accountValidTo; }
            set
            {
                if (_accountValidTo == value) return;
                _accountValidTo = value;
                raisePropertyChanged("AccountValidTo");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                raisePropertyChanged("Name");
            }
        }

        #endregion Properties



        #region INotifyPropertyChanged Members
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
