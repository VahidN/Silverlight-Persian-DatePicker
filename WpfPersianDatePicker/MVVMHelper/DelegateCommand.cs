﻿using System;
using System.Windows.Input;

namespace WpfPersianDatePicker.MVVMHelper
{
	// From http://johnpapa.net/silverlight/5-simple-steps-to-commanding-in-silverlight/
	/// <summary>
	/// Simplifies SL4/WPF commanding.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DelegateCommand<T> : ICommand
	{
		readonly Func<T, bool> _canExecute;
        readonly Action<T> _executeAction;

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecute = null)
        {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");

            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { if (_canExecute != null) CommandManager.RequerySuggested += value; }
            remove { if (_canExecute != null) CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction((T)parameter);
        }
	}
}
